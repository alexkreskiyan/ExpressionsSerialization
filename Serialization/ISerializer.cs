using System.Linq.Expressions;
using ExpressionsSerialization.Nodes;

namespace ExpressionsSerialization.Serialization
{
    public interface ISerializer
    {
        INode Serialize(Expression expression);

        INode Serialize(INode parent, Expression expression);

        Expression Deserialize(IDeserializationContext context, INode node);

        Expression<T> Deserialize<T>(IDeserializationContext context, INode node);
    }
}