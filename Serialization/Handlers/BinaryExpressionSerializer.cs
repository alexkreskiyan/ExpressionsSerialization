using System.Linq.Expressions;
using ExpressionsSerialization.Nodes;

namespace ExpressionsSerialization.Serialization.Handlers
{
    public class BinaryExpressionSerializer : ExpressionSerializer<BinaryExpression>
    {
        private readonly ISerializer serializer;

        public BinaryExpressionSerializer(ISerializer serializer)
        {
            this.serializer = serializer;
        }

        public override INode Serialize(BinaryExpression expression)
        {
            var node = new BinaryNode();
            node.NodeType = expression.NodeType;
            node.Left = serializer.Serialize(node, expression.Left);
            node.Right = serializer.Serialize(node, expression.Right);

            return node;
        }
    }
}