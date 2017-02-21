using System.Linq.Expressions;
using ExpressionsSerialization.Nodes;

namespace ExpressionsSerialization.Serialization.Handlers
{
    public class BinaryExpressionSerializer : ExpressionSerializer<BinaryNode, BinaryExpression>
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

        public override Expression Deserialize(IDeserializationContext context, BinaryNode node)
        {
            return Expression.MakeBinary(
                node.NodeType,
                serializer.Deserialize(context, node.Left),
                serializer.Deserialize(context, node.Right)
            );
        }
    }
}