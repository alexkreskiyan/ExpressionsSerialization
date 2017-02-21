using System.Collections.Generic;
using System.Linq.Expressions;

namespace ExpressionsSerialization.Serialization
{
    public interface IDeserializationContext
    {
        Dictionary<string, ParameterExpression> Parameters { get; }
    }
}