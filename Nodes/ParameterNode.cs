using System;

namespace ExpressionsSerialization.Nodes
{
    public class ParameterNode : INode
    {
        public Type Type { get; set; }

        public string Name { get; set; }
    }
}