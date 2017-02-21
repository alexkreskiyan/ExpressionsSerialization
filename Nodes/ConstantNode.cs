using System;
using System.Linq.Expressions;

namespace ExpressionsSerialization.Nodes
{
    public class ConstantNode : INode
    {
        public ExpressionType NodeType { get; set; }

        public Type Type { get; set; }

        public object Value { get; set; }
    }
}