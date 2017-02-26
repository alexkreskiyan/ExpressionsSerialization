using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.Serialization.Formatters;
using ExpressionsSerialization.ExpressionNodes;
using ExpressionsSerialization.ExpressionSerializers;
using ExpressionsSerialization.Models;
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
            var raw = Serialize((User user, int age) => user.Age >= age);
            var serialized = ToJSON(raw);

            Console.WriteLine($"Serialized expression:");
            Console.WriteLine(serialized);

            var deserialized = FromJSON(serialized);
            var result = Deserialize<User, int>(deserialized);

            var action = result.Compile();
            var answer = action(GetUser("Alex", 18), 18);
            Console.WriteLine(answer);

            var compiledResult = Compile<User>(result, new Dictionary<string, object>() { { "age", 18 } });

            var compiledAction = compiledResult.Compile();
            var compiledAnswer = compiledAction(GetUser("Alex", 18));
            Console.WriteLine(answer);
        }

        private static LambdaExpressionNode Serialize<T1, T2>(Expression<Func<T1, T2, bool>> expression)
        {
            Console.WriteLine($"Serializing {expression}");

            return (LambdaExpressionNode)provider.GetRequiredService<ISerializer>().Serialize(expression);
        }

        private static Expression<Func<T1, T2, bool>> Deserialize<T1, T2>(IExpressionNode node)
        {
            var expression = provider.GetRequiredService<ISerializer>()
                .Deserialize<Func<T1, T2, bool>>(provider.GetRequiredService<IDeserializationContext>(), node);

            Console.WriteLine($"Deserialized into {expression}");

            return expression;
        }

        private static Expression<Func<T, bool>> Compile<T>(
            Expression expression,
            IDictionary<string, object> parameterValues
        )
        {
            var compiled = provider.GetRequiredService<ISerializer>()
                .Compile<Func<T, bool>>(CompilationContext.Create(parameterValues), expression);

            Console.WriteLine($"Compiled {expression} to {compiled}");

            return compiled;
        }

        private static string ToJSON(IExpressionNode node)
        {
            return JsonConvert.SerializeObject(node, new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.Objects,
                TypeNameAssemblyFormat = FormatterAssemblyStyle.Simple
            });
        }

        private static IExpressionNode FromJSON(string json)
        {
            return JsonConvert.DeserializeObject<LambdaExpressionNode>(json, new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.Objects
            });
        }

        private static IServiceProvider GetServiceProvider()
        {
            var services = new ServiceCollection();

            services.AddSingleton<ISerializer, Serializer>();
            services.AddSingleton<ITransitionMap, TransitionMap>();
            services.AddSingleton<IDeserializationContext, DeserializationContext>();

            //register serilizers
            services.AddSingleton<IExpressionSerializer<LambdaExpression>, LambdaExpressionSerializer>();
            services.AddSingleton<IExpressionSerializer<ParameterExpression>, ParameterExpressionSerializer>();
            services.AddSingleton<IExpressionSerializer<BinaryExpression>, BinaryExpressionSerializer>();
            services.AddSingleton<IExpressionSerializer<MemberExpression>, MemberExpressionSerializer>();
            services.AddSingleton<IExpressionSerializer<ConstantExpression>, ConstantExpressionSerializer>();

            //register deserilizers
            services.AddSingleton<IExpressionDeserializer<LambdaExpressionNode>, LambdaExpressionSerializer>();
            services.AddSingleton<IExpressionDeserializer<ParameterExpressionNode>, ParameterExpressionSerializer>();
            services.AddSingleton<IExpressionDeserializer<BinaryExpressionNode>, BinaryExpressionSerializer>();
            services.AddSingleton<IExpressionDeserializer<MemberExpressionNode>, MemberExpressionSerializer>();
            services.AddSingleton<IExpressionDeserializer<ConstantExpressionNode>, ConstantExpressionSerializer>();

            //register compilers
            services.AddSingleton<IExpressionCompiler<LambdaExpression>, LambdaExpressionSerializer>();
            services.AddSingleton<IExpressionCompiler<ParameterExpression>, ParameterExpressionSerializer>();
            services.AddSingleton<IExpressionCompiler<BinaryExpression>, BinaryExpressionSerializer>();
            services.AddSingleton<IExpressionCompiler<MemberExpression>, MemberExpressionSerializer>();
            services.AddSingleton<IExpressionCompiler<ConstantExpression>, ConstantExpressionSerializer>();

            return services.BuildServiceProvider();
        }

        private static User GetUser(string name, int age)
        {
            return new User
            {
                Name = name,
                Age = age
            };
        }
    }
}