using System.Linq.Expressions;
using ExpressionsSerialization.Nodes;

namespace ExpressionsSerialization.Serialization.Handlers
{
    public class ParameterExpressionSerializer : ExpressionSerializer<ParameterNode, ParameterExpression>
    {
        public override INode Serialize(ParameterExpression expression)
        {
            var node = new ParameterNode();

            node.NodeType = expression.NodeType;
            node.Type = expression.Type;
            node.Name = expression.Name;

            return node;
        }

        public override Expression Deserialize(ParameterNode node)
        {
            return Expression.Parameter(node.Type, node.Name);
        }
    }
}