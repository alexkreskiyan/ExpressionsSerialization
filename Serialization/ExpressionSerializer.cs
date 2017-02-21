using SystemExpression = System.Linq.Expressions.Expression;
using ExpressionsSerialization.Symbols;
using ExpressionsSerialization.Symbols.Helpers;

namespace ExpressionsSerialization.Serialization
{
    public class ExpressionSerializer
    {
        public ISymbol Serialize(SystemExpression expression)
            => new SymbolFactory(new TransitionMap()).Create(expression);

        public SystemExpression Deserialize(ISymbol expression)
            => expression.ToExpression();
    }
}