using System.Linq.Expressions;

namespace ExpressionsSerialization.Nodes
{
    public class LambdaNode : INode
    {
        public ExpressionType NodeType { get; set; } = ExpressionType.Lambda;

        public INode[] Parameters { get; set; }

        public INode Body { get; set; }
    }
}