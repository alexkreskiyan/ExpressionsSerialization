using System.Linq.Expressions;
using ExpressionsSerialization.ExpressionNodes;

namespace ExpressionsSerialization.ExpressionSerializers
{
    public abstract class ExpressionSerializer<TNode, TExpression>
        : IExpressionSerializer<TExpression>, IExpressionDeserializer<TNode>, IExpressionCompiler<TExpression>
        where TNode : IExpressionNode
        where TExpression : Expression
    {
        public abstract IExpressionNode Serialize(TExpression expression);

        public IExpressionNode Serialize(Expression expression)
            => Serialize((TExpression)expression);

        public abstract Expression Deserialize(IDeserializationContext context, TNode node);

        public Expression Deserialize(IDeserializationContext context, IExpressionNode node)
            => Deserialize(context, (TNode)node);

        public abstract Expression Compile(ICompilationContext context, TExpression expression);

        public Expression Compile(ICompilationContext context, Expression expression)
            => Compile(context, (TExpression)expression);
    }

    public interface IExpressionSerializer<TExpression> : IExpressionSerializer
        where TExpression : Expression
    {
        IExpressionNode Serialize(TExpression expression);
    }

    public interface IExpressionDeserializer<TNode> : IExpressionDeserializer
        where TNode : IExpressionNode
    {
        Expression Deserialize(IDeserializationContext context, TNode node);
    }

    public interface IExpressionCompiler<TExpression> : IExpressionCompiler
        where TExpression : Expression
    {
        Expression Compile(ICompilationContext context, TExpression expression);
    }

    public interface IExpressionSerializer
    {
        IExpressionNode Serialize(Expression expression);
    }

    public interface IExpressionDeserializer
    {
        Expression Deserialize(IDeserializationContext context, IExpressionNode node);
    }

    public interface IExpressionCompiler
    {
        Expression Compile(ICompilationContext context, Expression expression);
    }
}