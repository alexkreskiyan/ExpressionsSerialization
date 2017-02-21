using SystemExpression = System.Linq.Expressions.Expression;
using ExpressionsSerialization.Expressions;

namespace ExpressionsSerialization.Serialization
{
    public class ExpressionSerializer
    {
        public IExpression Serialize(SystemExpression expression)
            => new ExpressionFactory(new TransitionMap()).Create(expression);

        public SystemExpression Deserialize(IExpression expression)
            => expression.ToExpression();
    }
}