using System;
using System.Linq.Expressions;
using ExpressionsSerialization.Symbols.Helpers;

namespace ExpressionsSerialization.Symbols
{
    public class ParameterSymbol : ISymbol
    {
        public string Name { get; set; }

        public Type Type { get; set; }

        public ExpressionType NodeType { get; } = ExpressionType.Parameter;

        public ParameterSymbol()
        { }

        public ParameterSymbol(ISymbolFactory factory, System.Linq.Expressions.ParameterExpression expression)
        {
            Name = expression.Name;
            Type = expression.Type;
        }

        public Expression ToExpression()
        {
            return Expression.Parameter(Type, Name);
        }
    }
}