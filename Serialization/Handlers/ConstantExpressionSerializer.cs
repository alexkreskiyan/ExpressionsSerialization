using System;
using System.Linq.Expressions;
using ExpressionsSerialization.Nodes;

namespace ExpressionsSerialization.Serialization.Handlers
{
    public class ConstantExpressionSerializer : ExpressionSerializer<ConstantNode, ConstantExpression>
    {
        public override INode Serialize(ConstantExpression expression)
        {
            var node = new ConstantNode();

            node.NodeType = expression.NodeType;
            node.Type = expression.Type;
            node.Value = expression.Value;

            return node;
        }

        public override Expression Deserialize(ConstantNode node)
        {
            return Expression.Constant(Convert.ChangeType(node.Value, node.Type), node.Type);
        }
    }
}