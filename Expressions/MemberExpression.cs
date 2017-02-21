using System;
using System.Linq.Expressions;

namespace ExpressionsSerialization.Expressions
{
    public class MemberExpression : IExpression
    {
        public ExpressionType NodeType { get; }

        public Type MemberDeclaringType { get; }

        public string MemberName { get; }

        public MemberExpression(IExpressionFactory factory, System.Linq.Expressions.MemberExpression expression)
        {
            NodeType = expression.NodeType;
            MemberDeclaringType = expression.Member.DeclaringType;
            MemberName = expression.Member.Name;
        }

        public Expression ToExpression()
        {
            throw new NotImplementedException();
        }
    }
}