using System;
using System.Linq.Expressions;

namespace ExpressionsSerialization.ExpressionNodes
{
    public class MemberExpressionNode : IExpressionNode
    {
        public ExpressionType NodeType { get; set; }

        public IExpressionNode Expression { get; set; }

        public Type MemberDeclaringType { get; set; }

        public string MemberName { get; set; }
    }
}