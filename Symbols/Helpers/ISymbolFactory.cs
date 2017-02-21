using System.Linq.Expressions;

namespace ExpressionsSerialization.Symbols.Helpers
{
    public interface ISymbolFactory
    {
        ISymbol Create(Expression expression);

        ISymbol Create(ISymbol parent, Expression expression);
    }
}