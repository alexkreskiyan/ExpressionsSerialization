using System.Collections.Generic;

namespace ExpressionsSerialization
{
    public interface ICompilationContext
    {
        IReadOnlyDictionary<string, object> ParametersValues { get; }
    }
}