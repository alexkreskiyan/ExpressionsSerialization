using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ExpressionsSerialization
{
    public class CompilationContext : ICompilationContext
    {
        public static ICompilationContext Create(IDictionary<string, object> parametersValues)
            => new CompilationContext(parametersValues);

        public IReadOnlyDictionary<string, object> ParametersValues { get; }

        private CompilationContext(IDictionary<string, object> parametersValues)
        {
            ParametersValues = new ReadOnlyDictionary<string, object>(parametersValues);
        }
    }
}