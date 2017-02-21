using System.Linq.Expressions;

namespace ExpressionsSerialization.Expressions
{
    public interface IExpressionFactory
    {
        IExpression Create(Expression expression);

        IExpression Create(IExpression parent, Expression expression);
    }
}