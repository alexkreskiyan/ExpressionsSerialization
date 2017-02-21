using System;
using System.Linq.Expressions;

namespace ExpressionsSerialization.Nodes
{
    public class MemberNode : INode
    {
        public ExpressionType NodeType { get; set; }

        public INode Expression { get; set; }

        public Type MemberDeclaringType { get; set; }

        public string MemberName { get; set; }
    }
}