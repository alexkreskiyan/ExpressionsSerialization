using System;
using System.Linq.Expressions;
using ExpressionsSerialization.Symbols.Helpers;

namespace ExpressionsSerialization.Symbols
{
    public class ConstantSymbol : ISymbol
    {
        public ExpressionType NodeType { get; }

        public object Value { get; }

        public Type Type { get; }

        public ConstantSymbol(ISymbolFactory factory, System.Linq.Expressions.ConstantExpression expression)
        {
            NodeType = expression.NodeType;
            Value = expression.Value;
            Type = expression.Type;
        }

        public Expression ToExpression()
        {
            return Expression.Constant(Value, Type);
        }
    }
}