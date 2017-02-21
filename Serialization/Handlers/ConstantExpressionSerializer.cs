using System.Linq.Expressions;
using ExpressionsSerialization.Nodes;

namespace ExpressionsSerialization.Serialization.Handlers
{
    public class ConstantExpressionSerializer : ExpressionSerializer<ConstantExpression>
    {
        public override INode Serialize(ConstantExpression expression)
        {
            var node = new ConstantNode();

            node.NodeType = expression.NodeType;
            node.Value = expression.Value;

            return node;
        }
    }
}