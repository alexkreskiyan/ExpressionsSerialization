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

        public override Expression Deserialize(IDeserializationContext context, ParameterNode node)
        {
            if (context.Parameters.ContainsKey(node.Name))
                return context.Parameters[node.Name];

            var parameter = Expression.Parameter(node.Type, node.Name);
            context.Parameters.Add(parameter.Name, parameter);

            return parameter;
        }
    }
}