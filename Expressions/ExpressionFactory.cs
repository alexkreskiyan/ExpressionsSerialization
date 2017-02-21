using System;
using System.Linq.Expressions;
using System.Reflection;

namespace ExpressionsSerialization.Expressions
{
    public class ExpressionFactory : IExpressionFactory
    {
        private readonly ITransitionMap transitionMap;

        public ExpressionFactory(ITransitionMap transitionMap)
        {
            this.transitionMap = transitionMap;
        }

        public IExpression Create(Expression expression)
            => new LambdaExpression(this, (System.Linq.Expressions.LambdaExpression)expression);


        public IExpression Create(IExpression parent, Expression expression)
        {
            transitionMap.EnsureValidTransition(parent.NodeType, expression.NodeType);

            switch (expression)
            {
                case System.Linq.Expressions.MemberExpression memberExpression:
                    if (memberExpression.Member.MemberType == MemberTypes.Property)
                        return new MemberExpression(this, memberExpression);
                    break;
                case System.Linq.Expressions.BinaryExpression binaryExpression:
                    return new BinaryExpression(this, binaryExpression);
                case System.Linq.Expressions.ConstantExpression constantExpression:
                    return new ConstantExpression(this, constantExpression);
            }

            throw new InvalidOperationException($"Expression type {expression.Type} is not supported");

            // switch (expression.NodeType)
            // {
            //     case ExpressionType.OrElse:
            //     case ExpressionType.AndAlso:
            //         return CreateComplex(expression);
            //     default:
            //         throw new InvalidOperationException($"Expression type {expression.NodeType} is not supported");
            // }
        }
        private IExpression CreateComplex(Expression expression)
        {
            switch (expression)
            {
                case System.Linq.Expressions.BinaryExpression e:
                    return new BinaryExpression(this, e);
                default:
                    throw new InvalidOperationException($"Expression type {expression.NodeType} is not supported");
            }
        }
    }
}