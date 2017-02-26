using System.Linq.Expressions;

namespace ExpressionsSerialization.ExpressionNodes
{
    public interface IExpressionNode
    {
        ExpressionType NodeType { get; set; }
    }
}