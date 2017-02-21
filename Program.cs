using System;
using System.Linq.Expressions;
using System.Runtime.Serialization.Formatters;
using ExpressionsSerialization.Models;
using ExpressionsSerialization.Nodes;
using ExpressionsSerialization.Serialization;
using ExpressionsSerialization.Serialization.Handlers;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace ExpressionsSerialization
{
    public class Program
    {
        private static IServiceProvider provider;

        public static void Main()
        {
            provider = GetServiceProvider();
            var raw = Serialize(
                user => user.Age >= 18
            );
            var serialized = JsonConvert.SerializeObject(raw, new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.Objects,
                TypeNameAssemblyFormat = FormatterAssemblyStyle.Simple
            });
            Console.WriteLine(serialized);
            var deserialized = JsonConvert.DeserializeObject<LambdaNode>(serialized, new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.Objects
            });
        }

        private static INode Serialize(Expression<Func<User, bool>> expression)
        {
            return provider.GetRequiredService<Serialization.ISerializer>().Serialize(expression);
        }

        private static IServiceProvider GetServiceProvider()
        {
            var services = new ServiceCollection();

            services.AddSingleton<Serialization.ISerializer, Serializer>();
            services.AddSingleton<ITransitionMap, TransitionMap>();

            services.AddSingleton<ExpressionSerializer<LambdaExpression>, LambdaExpressionSerializer>();
            services.AddSingleton<ExpressionSerializer<ParameterExpression>, ParameterExpressionSerializer>();
            services.AddSingleton<ExpressionSerializer<BinaryExpression>, BinaryExpressionSerializer>();
            services.AddSingleton<ExpressionSerializer<MemberExpression>, MemberExpressionSerializer>();
            services.AddSingleton<ExpressionSerializer<ConstantExpression>, ConstantExpressionSerializer>();

            return services.BuildServiceProvider();
        }
    }
}