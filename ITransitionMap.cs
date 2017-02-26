using System.Linq.Expressions;

namespace ExpressionsSerialization
{
    public interface ITransitionMap
    {
        void EnsureValidTransition(ExpressionType parent, ExpressionType child);
    }
}