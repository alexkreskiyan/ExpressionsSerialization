using System.Linq.Expressions;
using ExpressionsSerialization.ExpressionNodes;

namespace ExpressionsSerialization.ExpressionSerializers
{
    public class BinaryExpressionSerializer : ExpressionSerializer<BinaryExpressionNode, BinaryExpression>
    {
        private readonly ISerializer serializer;

        public BinaryExpressionSerializer(ISerializer serializer)
        {
            this.serializer = serializer;
        }

        public override IExpressionNode Serialize(BinaryExpression expression)
        {
            var node = new BinaryExpressionNode();
            node.NodeType = expression.NodeType;
            node.Left = serializer.Serialize(node, expression.Left);
            node.Right = serializer.Serialize(node, expression.Right);

            return node;
        }

        public override Expression Deserialize(IDeserializationContext context, BinaryExpressionNode node)
        {
            return Expression.MakeBinary(
                node.NodeType,
                serializer.Deserialize(context, node.Left),
                serializer.Deserialize(context, node.Right)
            );
        }
    }
}