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

        public abstract Expression Deserialize(TNode node);

        public Expression Deserialize(INode node) => Deserialize((TNode)node);
    }

    public interface IExpressionSerializer<TExpression> : IExpressionSerializer
        where TExpression : Expression
    {
        INode Serialize(TExpression expression);
    }

    public interface IExpressionDeserializer<TNode> : IExpressionDeserializer
        where TNode : INode
    {
        Expression Deserialize(TNode node);
    }

    public interface IExpressionSerializer
    {
        INode Serialize(Expression expression);
    }

    public interface IExpressionDeserializer
    {
        Expression Deserialize(INode node);
    }
}