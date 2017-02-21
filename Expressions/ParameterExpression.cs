using System;

namespace ExpressionsSerialization.Expressions
{
    public class ParameterExpression
    {
        public string Name { get; }

        public Type Type { get; }

        public ParameterExpression(string name, Type type)
        {
            Name = name;
            Type = type;
        }
    }
}