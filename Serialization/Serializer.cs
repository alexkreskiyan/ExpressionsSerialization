using ExpressionsSerialization.Nodes;
using System;
using System.Linq.Expressions;
using ExpressionsSerialization.Serialization.Handlers;
using System.Reflection;

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
                    typeof(ExpressionSerializer<>).MakeGenericType(expressionType)
                );
                expressionType = expressionType.GetTypeInfo().BaseType;
            }
            while (handler == null && expressionType != typeof(Expression));

            if (handler == null)
                throw new InvalidOperationException(
                    $"No serialization handler registered for expression type {expression.GetType()}"
                );

            return (handler as Handlers.IExpressionSerializer).Serialize(expression);
        }

        public INode Serialize(INode parent, Expression expression)
        {
            transitionMap.EnsureValidTransition(parent.NodeType, expression.NodeType);

            return Serialize(expression);
        }

        public Expression Deserialize(INode node)
        {
            throw new NotImplementedException();
        }

        // public ISymbol Serialize(SystemExpression expression)
        //     => new SymbolFactory(new TransitionMap()).Create(expression);

        // public SystemExpression Deserialize(ISymbol expression)
        //     => expression.ToExpression();
    }
}