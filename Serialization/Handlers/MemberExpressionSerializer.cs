using System.Linq.Expressions;
using System.Reflection;
using ExpressionsSerialization.Nodes;

namespace ExpressionsSerialization.Serialization.Handlers
{
    public class MemberExpressionSerializer : ExpressionSerializer<MemberNode, MemberExpression>
    {
        private readonly ISerializer serializer;

        public MemberExpressionSerializer(ISerializer serializer)
        {
            this.serializer = serializer;
        }

        public override INode Serialize(MemberExpression expression)
        {
            var node = new MemberNode();

            node.NodeType = expression.NodeType;
            node.Expression = serializer.Serialize(node, expression.Expression);
            node.MemberDeclaringType = expression.Member.DeclaringType;
            node.MemberName = expression.Member.Name;

            return node;
        }

        public override Expression Deserialize(IDeserializationContext context, MemberNode node)
        {
            return System.Linq.Expressions.Expression.MakeMemberAccess(
                serializer.Deserialize(context, node.Expression),
                node.MemberDeclaringType.GetTypeInfo().GetProperty(node.MemberName)
            );
        }
    }
}