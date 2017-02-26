using System.Linq;
using System.Linq.Expressions;
using ExpressionsSerialization.ExpressionNodes;

namespace ExpressionsSerialization.ExpressionSerializers
{
    public class LambdaExpressionSerializer : ExpressionSerializer<LambdaExpressionNode, LambdaExpression>
    {
        private readonly ISerializer serializer;

        public LambdaExpressionSerializer(ISerializer serializer)
        {
            this.serializer = serializer;
        }

        public override IExpressionNode Serialize(LambdaExpression expression)
        {
            var node = new LambdaExpressionNode();

            node.Parameters = expression.Parameters
                .Select(parameter => serializer.Serialize(node, parameter))
                .ToArray();

            node.Body = serializer.Serialize(node, expression.Body);

            return node;
        }

        public override Expression Deserialize(IDeserializationContext context, LambdaExpressionNode node)
        {
            var parameters = node.Parameters
                .Select(parameter => serializer.Deserialize(context, parameter) as ParameterExpression);

            return Expression.Lambda(
                serializer.Deserialize(context, node.Body),
                false,
                parameters
            );
        }

        public override Expression Compile(ICompilationContext context, LambdaExpression expression)
        {
            var parameters = expression.Parameters
                .Where(parameter => !context.ParametersValues.ContainsKey(parameter.Name))
                .Select(parameter => serializer.Compile(context, parameter) as ParameterExpression);

            return Expression.Lambda(
                serializer.Compile(context, expression.Body),
                false,
                parameters
            );
        }
    }
}