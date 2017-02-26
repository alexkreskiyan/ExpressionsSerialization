using System.Linq.Expressions;
using System.Reflection;
using ExpressionsSerialization.ExpressionNodes;

namespace ExpressionsSerialization.ExpressionSerializers
{
    public class MemberExpressionSerializer : ExpressionSerializer<MemberExpressionNode, MemberExpression>
    {
        private readonly ISerializer serializer;

        public MemberExpressionSerializer(ISerializer serializer)
        {
            this.serializer = serializer;
        }

        public override IExpressionNode Serialize(MemberExpression expression)
        {
            var node = new MemberExpressionNode();

            node.NodeType = expression.NodeType;
            node.Expression = serializer.Serialize(node, expression.Expression);
            node.MemberDeclaringType = expression.Member.DeclaringType;
            node.MemberName = expression.Member.Name;

            return node;
        }

        public override Expression Deserialize(IDeserializationContext context, MemberExpressionNode node)
        {
            return System.Linq.Expressions.Expression.MakeMemberAccess(
                serializer.Deserialize(context, node.Expression),
                node.MemberDeclaringType.GetTypeInfo().GetProperty(node.MemberName)
            );
        }
    }
}