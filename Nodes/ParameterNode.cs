using System;
using System.Linq.Expressions;

namespace ExpressionsSerialization.Nodes
{
    public class ParameterNode : INode
    {
        public ExpressionType NodeType { get; set; }

        public Type Type { get; set; }

        public string Name { get; set; }
    }
}