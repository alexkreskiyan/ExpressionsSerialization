using System.Linq.Expressions;

namespace ExpressionsSerialization.Expressions
{
    public interface ITransitionMap
    {
        void EnsureValidTransition(ExpressionType parent, ExpressionType child);
    }
}