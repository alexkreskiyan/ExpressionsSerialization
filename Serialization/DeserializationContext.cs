using System.Collections.Generic;
using System.Linq.Expressions;

namespace ExpressionsSerialization.Serialization
{
    public class DeserializationContext : IDeserializationContext
    {
        public Dictionary<string, ParameterExpression> Parameters { get; } = new Dictionary<string, ParameterExpression>();
    }
}