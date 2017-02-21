using System.Linq.Expressions;

namespace ExpressionsSerialization.Symbols
{
    public interface ISymbol
    {
        ExpressionType NodeType { get; }

        Expression ToExpression();
    }
}