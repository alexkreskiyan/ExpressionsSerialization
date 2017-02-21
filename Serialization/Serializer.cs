using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using ExpressionsSerialization.Nodes;
using ExpressionsSerialization.Serialization.Handlers;

namespace ExpressionsSerialization.Serialization
{
    public class Serializer : ISerializer
    {
        private readonly IServiceProvider serviceProvider;
        private readonly ITransitionMap transitionMap;

        public Serializer(
            IServiceProvider serviceProvider,
            ITransitionMap transitionMap
        )
        {
            this.serviceProvider = serviceProvider;
            this.transitionMap = transitionMap;
        }

        public INode Serialize(Expression expression)
        {
            var expressionType = expression.GetType();
            object handler;

            do
            {
                handler = serviceProvider.GetService(
                    typeof(IExpressionSerializer<>).MakeGenericType(expressionType)
                );
                expressionType = expressionType.GetTypeInfo().BaseType;
            }
            while (handler == null && expressionType != typeof(Expression));

            if (handler == null)
                throw new InvalidOperationException(
                    $"No serializer registered for expression type {expression.GetType()}"
                );

            return (handler as IExpressionSerializer).Serialize(expression);
        }

        public INode Serialize(INode parent, Expression expression)
        {
            transitionMap.EnsureValidTransition(parent.NodeType, expression.NodeType);

            return Serialize(expression);
        }

        public Expression Deserialize(IDeserializationContext context, INode node)
        {
            var handler = serviceProvider.GetService(
                typeof(IExpressionDeserializer<>).MakeGenericType(node.GetType())
            );

            if (handler == null)
                throw new InvalidOperationException(
                    $"No deserializer registered for node type {node.GetType()}"
                );

            return (handler as IExpressionDeserializer).Deserialize(context, node);
        }

        public Expression<T> Deserialize<T>(IDeserializationContext context, INode node)
        {
            var raw = (LambdaExpression)Deserialize(context, node);

            var typeArguments = raw.Parameters
                .Select(parameter => parameter.Type)
                .ToList();

            if (raw.ReturnType != null)
                typeArguments.Add(raw.ReturnType);

            var resultType = raw.ReturnType == null
                ? Expression.GetActionType(typeArguments.ToArray())
                : Expression.GetFuncType(typeArguments.ToArray());

            var method = typeof(Expression)
                .GetMethods()
                .First(candidate =>
                {
                    var parameters = candidate.GetParameters();

                    return candidate.Name == "Lambda"
                        && candidate.GetGenericArguments().Length == 1
                        && parameters.Length == 2
                        && parameters[0].ParameterType == typeof(Expression)
                        && parameters[1].ParameterType == typeof(IEnumerable<ParameterExpression>);
                })
                .MakeGenericMethod(resultType);

            return (Expression<T>)method.Invoke(null, new object[] { raw.Body, raw.Parameters });
        }
    }
}