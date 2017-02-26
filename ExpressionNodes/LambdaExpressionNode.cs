using System.Linq.Expressions;

namespace ExpressionsSerialization.ExpressionNodes
{
    public class LambdaExpressionNode : IExpressionNode
    {
        public ExpressionType NodeType { get; set; } = ExpressionType.Lambda;

        public IExpressionNode[] Parameters { get; set; }

        public IExpressionNode Body { get; set; }
    }
}