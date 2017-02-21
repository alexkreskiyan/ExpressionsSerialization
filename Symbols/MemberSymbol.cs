using System;
using System.Linq.Expressions;
using System.Reflection;
using ExpressionsSerialization.Symbols.Helpers;

namespace ExpressionsSerialization.Symbols
{
    public class MemberSymbol : ISymbol
    {
        public ExpressionType NodeType { get; }

        public ISymbol Expression { get; }

        public Type MemberDeclaringType { get; }

        public string MemberName { get; }

        public MemberSymbol(ISymbolFactory factory, System.Linq.Expressions.MemberExpression expression)
        {
            NodeType = expression.NodeType;
            Expression = factory.Create(this, expression.Expression);
            MemberDeclaringType = expression.Member.DeclaringType;
            MemberName = expression.Member.Name;
        }

        public Expression ToExpression()
        {
            return System.Linq.Expressions.Expression.MakeMemberAccess(
                Expression.ToExpression(),
                MemberDeclaringType.GetTypeInfo().GetProperty(MemberName)
            );
        }
    }
}