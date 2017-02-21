using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using SystemExpression = System.Linq.Expressions.LambdaExpression;

namespace ExpressionsSerialization.Expressions
{
    public class LambdaExpression : IExpression
    {
        public ExpressionType NodeType { get; } = ExpressionType.Lambda;

        public ReadOnlyDictionary<string, ParameterExpression> Parameters { get; }

        public IExpression Body { get; }

        public LambdaExpression(IExpressionFactory factory, SystemExpression expression)
        {
            Parameters = new ReadOnlyDictionary<string, ParameterExpression>(
                expression.Parameters
                    .Select(parameter => new ParameterExpression(parameter.Name, parameter.Type))
                    .ToDictionary(parameter => parameter.Name, parameter => parameter)
            );

            Body = factory.Create(this, expression.Body);
        }

        public Expression ToExpression()
        {
            throw new NotImplementedException();
        }
    }
}