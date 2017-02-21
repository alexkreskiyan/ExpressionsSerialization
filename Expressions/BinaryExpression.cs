using System;
using System.Linq.Expressions;

namespace ExpressionsSerialization.Expressions
{
    public class BinaryExpression : IExpression
    {
        public ExpressionType NodeType { get; }

        public IExpression Left { get; }

        public IExpression Right { get; }

        public BinaryExpression(IExpressionFactory factory, System.Linq.Expressions.BinaryExpression expression)
        {
            NodeType = expression.NodeType;
            Left = factory.Create(this, expression.Left);
            Right = factory.Create(this, expression.Right);
        }

        public Expression ToExpression()
        {
            throw new NotImplementedException();
        }
    }
}