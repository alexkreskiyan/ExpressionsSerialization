using System.Linq.Expressions;

namespace ExpressionsSerialization.Serialization
{
    public interface ITransitionMap
    {
        void EnsureValidTransition(ExpressionType parent, ExpressionType child);
    }
}