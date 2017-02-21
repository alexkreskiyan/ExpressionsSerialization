using System.Linq.Expressions;
using ExpressionsSerialization.Nodes;

namespace ExpressionsSerialization.Serialization.Handlers
{
    public abstract class ExpressionSerializer<TExpression> : IExpressionSerializer
        where TExpression : Expression
    {
        public abstract INode Serialize(TExpression expression);

        public INode Serialize(Expression expression) => Serialize((TExpression)expression);
    }

    public interface IExpressionSerializer
    {
        INode Serialize(Expression expression);
    }
}