using System;
using System.Linq.Expressions;
using ExpressionsSerialization.ExpressionNodes;

namespace ExpressionsSerialization.ExpressionSerializers
{
    public class ConstantExpressionSerializer : ExpressionSerializer<ConstantExpressionNode, ConstantExpression>
    {
        public override IExpressionNode Serialize(ConstantExpression expression)
        {
            var node = new ConstantExpressionNode();

            node.NodeType = expression.NodeType;
            node.Type = expression.Type;
            node.Value = expression.Value;

            return node;
        }

        public override Expression Deserialize(IDeserializationContext context, ConstantExpressionNode node)
        {
            return Expression.Constant(Convert.ChangeType(node.Value, node.Type), node.Type);
        }

        public override Expression Compile(ICompilationContext context, ConstantExpression expression)
        {
            return expression;
        }
    }
}