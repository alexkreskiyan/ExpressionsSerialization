using System.Linq.Expressions;
using ExpressionsSerialization.ExpressionNodes;

namespace ExpressionsSerialization
{
    public interface ISerializer
    {
        IExpressionNode Serialize(Expression expression);

        IExpressionNode Serialize(IExpressionNode parent, Expression expression);

        Expression Deserialize(IDeserializationContext context, IExpressionNode node);

        Expression<T> Deserialize<T>(IDeserializationContext context, IExpressionNode node);
    }
}