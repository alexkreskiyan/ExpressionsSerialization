using System;
using System.Linq.Expressions;
using System.Reflection;

namespace ExpressionsSerialization.Symbols.Helpers
{
    public class SymbolFactory : ISymbolFactory
    {
        private readonly ITransitionMap transitionMap;

        public SymbolFactory(ITransitionMap transitionMap)
        {
            this.transitionMap = transitionMap;
        }

        public ISymbol Create(Expression expression)
            => new LambdaSymbol(this, (LambdaExpression)expression);


        public ISymbol Create(ISymbol parent, Expression expression)
        {
            transitionMap.EnsureValidTransition(parent.NodeType, expression.NodeType);

            switch (expression)
            {
                case ParameterExpression parameterExpression:
                    return new ParameterSymbol(this, parameterExpression);
                case MemberExpression memberExpression:
                    if (memberExpression.Member.MemberType == MemberTypes.Property)
                        return new MemberSymbol(this, memberExpression);
                    break;
                case BinaryExpression binaryExpression:
                    return new BinarySymbol(this, binaryExpression);
                case ConstantExpression constantExpression:
                    return new ConstantSymbol(this, constantExpression);
            }

            throw new InvalidOperationException($"Expression type {expression.NodeType} is not supported");

            // switch (expression.NodeType)
            // {
            //     case ExpressionType.OrElse:
            //     case ExpressionType.AndAlso:
            //         return CreateComplex(expression);
            //     default:
            //         throw new InvalidOperationException($"Expression type {expression.NodeType} is not supported");
            // }
        }
        private ISymbol CreateComplex(Expression expression)
        {
            switch (expression)
            {
                case BinaryExpression e:
                    return new BinarySymbol(this, e);
                default:
                    throw new InvalidOperationException($"Expression type {expression.NodeType} is not supported");
            }
        }
    }
}