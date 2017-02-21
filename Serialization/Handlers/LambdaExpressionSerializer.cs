using System;
using System.Linq;
using System.Linq.Expressions;
using ExpressionsSerialization.Nodes;

namespace ExpressionsSerialization.Serialization.Handlers
{
    public class LambdaExpressionSerializer : ExpressionSerializer<LambdaNode, LambdaExpression>
    {
        private readonly ISerializer serializer;

        public LambdaExpressionSerializer(ISerializer serializer)
        {
            this.serializer = serializer;
        }

        public override INode Serialize(LambdaExpression expression)
        {
            var node = new LambdaNode();

            node.Parameters = expression.Parameters
                .Select(parameter => serializer.Serialize(node, parameter))
                .ToArray();

            node.Body = serializer.Serialize(node, expression.Body);

            return node;
        }

        public override Expression Deserialize(LambdaNode node)
        {
            throw new NotImplementedException();
        }
    }
}