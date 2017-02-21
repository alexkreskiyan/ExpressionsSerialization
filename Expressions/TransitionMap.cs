using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ExpressionsSerialization.Expressions
{
    public class TransitionMap : ITransitionMap
    {
        private static Dictionary<ExpressionType, ExpressionType[]> states { get; } = new Dictionary<ExpressionType, ExpressionType[]>();

        static TransitionMap()
        {
            Add(
                ExpressionType.Lambda,
                ExpressionType.AndAlso,
                ExpressionType.GreaterThanOrEqual
            );
            Add(
                ExpressionType.GreaterThanOrEqual,
                ExpressionType.MemberAccess,
                ExpressionType.Constant
            );
        }

        private static void Add(ExpressionType parentType, params ExpressionType[] childTypes)
            => states.Add(parentType, childTypes);

        public void EnsureValidTransition(ExpressionType parentType, ExpressionType childType)
        {
            if (!states[parentType].Contains(childType))
                throw new InvalidOperationException(
                    $"Expression type {childType} is not allowed in expression {parentType}"
                );
        }
    }
}