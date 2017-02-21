using System;
using System.Linq.Expressions;

namespace ExpressionsSerialization.Expressions
{
    public class ConstantExpression : IExpression
    {
        public ExpressionType NodeType { get; }

        public object Value { get; }

        public ConstantExpression(IExpressionFactory factory, System.Linq.Expressions.ConstantExpression expression)
        {
            NodeType = expression.NodeType;
            Value = expression.Value;
        }

        public Expression ToExpression()
        {
            throw new NotImplementedException();
        }
    }
}