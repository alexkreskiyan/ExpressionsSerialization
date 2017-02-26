using System;
using System.Linq.Expressions;

namespace ExpressionsSerialization.ExpressionNodes
{
    public class ConstantExpressionNode : IExpressionNode
    {
        public ExpressionType NodeType { get; set; }

        public Type Type { get; set; }

        public object Value { get; set; }
    }
}