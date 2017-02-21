using System.Linq;
using System.Linq.Expressions;
using ExpressionsSerialization.Symbols.Helpers;
using SystemExpression = System.Linq.Expressions.LambdaExpression;

namespace ExpressionsSerialization.Symbols
{
    public class LambdaSymbol : ISymbol
    {
        public ExpressionType NodeType { get; } = ExpressionType.Lambda;

        public ParameterSymbol[] Parameters { get; set; }

        public ISymbol Body { get; set; }

        public LambdaSymbol()
        { }

        public LambdaSymbol(ISymbolFactory factory, SystemExpression expression)
        {
            Parameters = expression.Parameters
                .Select(parameter => factory.Create(this, parameter) as ParameterSymbol)
                .ToArray();

            Body = factory.Create(this, expression.Body);
        }

        public Expression ToExpression()
        {
            return Expression.Lambda(
                Body.ToExpression(),
                false,
                Parameters.Select(parameter => parameter.ToExpression() as System.Linq.Expressions.ParameterExpression)
            );
        }
    }
}