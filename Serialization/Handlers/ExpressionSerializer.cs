using System.Linq.Expressions;
using ExpressionsSerialization.Nodes;

namespace ExpressionsSerialization.Serialization.Handlers
{
    public abstract class ExpressionSerializer<TNode, TExpression> : IExpressionSerializer<TExpression>, IExpressionDeserializer<TNode>
        where TNode : INode
        where TExpression : Expression
    {
        public abstract INode Serialize(TExpression expression);

        public INode Serialize(Expression expression) => Serialize((TExpression)expression);

        public abstract Expression Deserialize(IDeserializationContext context, TNode node);

        public Expression Deserialize(IDeserializationContext context, INode node) => Deserialize(context, (TNode)node);
    }

    public interface IExpressionSerializer<TExpression> : IExpressionSerializer
        where TExpression : Expression
    {
        INode Serialize(TExpression expression);
    }

    public interface IExpressionDeserializer<TNode> : IExpressionDeserializer
        where TNode : INode
    {
        Expression Deserialize(IDeserializationContext context, TNode node);
    }

    public interface IExpressionSerializer
    {
        INode Serialize(Expression expression);
    }

    public interface IExpressionDeserializer
    {
        Expression Deserialize(IDeserializationContext context, INode node);
    }
}