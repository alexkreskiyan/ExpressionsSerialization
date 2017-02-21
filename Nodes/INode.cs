using System.Linq.Expressions;

namespace ExpressionsSerialization.Nodes
{
    public interface INode
    {
        ExpressionType NodeType { get; set; }
    }
}