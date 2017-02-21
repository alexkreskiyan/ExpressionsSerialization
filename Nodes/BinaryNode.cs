using System.Linq.Expressions;

namespace ExpressionsSerialization.Nodes
{
    public class BinaryNode : INode
    {
        public ExpressionType NodeType { get; set; }

        public INode Left { get; set; }

        public INode Right { get; set; }
    }
}