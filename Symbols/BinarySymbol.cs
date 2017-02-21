using System.Linq.Expressions;
using ExpressionsSerialization.Symbols.Helpers;

namespace ExpressionsSerialization.Symbols
{
    public class BinarySymbol : ISymbol
    {
        public ExpressionType NodeType { get; }

        public ISymbol Left { get; }

        public ISymbol Right { get; }

        public BinarySymbol(ISymbolFactory factory, System.Linq.Expressions.BinaryExpression expression)
        {
            NodeType = expression.NodeType;
            Left = factory.Create(this, expression.Left);
            Right = factory.Create(this, expression.Right);
        }

        public Expression ToExpression()
        {
            return Expression.MakeBinary(
                NodeType,
                Left.ToExpression(),
                Right.ToExpression()
            );
        }
    }
}