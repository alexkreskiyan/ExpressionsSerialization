using System;
using System.Linq.Expressions;
using ExpressionsSerialization.ExpressionNodes;

namespace ExpressionsSerialization.ExpressionSerializers
{
    public class ParameterExpressionSerializer : ExpressionSerializer<ParameterExpressionNode, ParameterExpression>
    {
        public override IExpressionNode Serialize(ParameterExpression expression)
        {
            var node = new ParameterExpressionNode();

            node.NodeType = expression.NodeType;
            node.Type = expression.Type;
            node.Name = expression.Name;

            return node;
        }

        public override Expression Deserialize(IDeserializationContext context, ParameterExpressionNode node)
        {
            if (context.Parameters.ContainsKey(node.Name))
                return context.Parameters[node.Name];

            var parameter = Expression.Parameter(node.Type, node.Name);
            context.Parameters.Add(parameter.Name, parameter);

            return parameter;
        }

        public override Expression Compile(ICompilationContext context, ParameterExpression expression)
        {
            if (!context.ParametersValues.ContainsKey(expression.Name))
                return expression;

            return Expression.Constant(
                Convert.ChangeType(context.ParametersValues[expression.Name], expression.Type),
                expression.Type
            );
        }
    }
}