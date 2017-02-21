using System.Linq.Expressions;

namespace ExpressionsSerialization.Symbols.Helpers
{
    public interface ITransitionMap
    {
        void EnsureValidTransition(ExpressionType parent, ExpressionType child);
    }
}