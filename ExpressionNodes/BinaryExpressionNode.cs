using System.Linq.Expressions;

namespace ExpressionsSerialization.ExpressionNodes
{
    public class BinaryExpressionNode : IExpressionNode
    {
        public ExpressionType NodeType { get; set; }

        public IExpressionNode Left { get; set; }

        public IExpressionNode Right { get; set; }
    }
}