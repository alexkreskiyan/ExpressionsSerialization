using System.Linq.Expressions;

namespace ExpressionsSerialization.Expressions
{
    public interface IExpression
    {
        ExpressionType NodeType { get; }

        Expression ToExpression();
    }
}