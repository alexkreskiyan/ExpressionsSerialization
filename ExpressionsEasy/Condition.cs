using System.Collections.Generic;

namespace ExpressionsSerialization.SymbolsEasy
{
    public enum ConditionType
    {
        And,
        Or
    }

    public class Condition : ISymbol
    {
        public static Condition And(params ISymbol[] expressions)
            => new Condition(ConditionType.And, expressions);

        public static Condition Or(params ISymbol[] expressions)
            => new Condition(ConditionType.Or, expressions);

        public IEnumerable<ISymbol> expressions { get; }

        private Condition(ConditionType type, ISymbol[] expressions)
        {
            this.expressions = expressions;
        }
    }
}