using System.Collections.Generic;

namespace ExpressionsSerialization.ExpressionsEasy
{
    public enum ConditionType
    {
        And,
        Or
    }

    public class Condition : IExpression
    {
        public static Condition And(params IExpression[] expressions)
            => new Condition(ConditionType.And, expressions);

        public static Condition Or(params IExpression[] expressions)
            => new Condition(ConditionType.Or, expressions);

        public IEnumerable<IExpression> expressions { get; }

        private Condition(ConditionType type, IExpression[] expressions)
        {
            this.expressions = expressions;
        }
    }
}