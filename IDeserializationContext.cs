using System.Collections.Generic;
using System.Linq.Expressions;

namespace ExpressionsSerialization
{
    public interface IDeserializationContext
    {
        Dictionary<string, ParameterExpression> Parameters { get; }
    }
}