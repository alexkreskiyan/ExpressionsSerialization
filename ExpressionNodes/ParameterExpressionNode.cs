using System;
using System.Linq.Expressions;

namespace ExpressionsSerialization.ExpressionNodes
{
    public class ParameterExpressionNode : IExpressionNode
    {
        public ExpressionType NodeType { get; set; }

        public Type Type { get; set; }

        public string Name { get; set; }
    }
}