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
            var result = Deserialize(deserialized);
            var action = result.Compile();
            var answer = action.DynamicInvoke(GetUser("Alex", 15));
            Console.WriteLine(serialized);
        }

        private static INode Serialize(Expression<Func<User, bool>> expression)
        {
            return provider.GetRequiredService<Serialization.ISerializer>().Serialize(expression);
        }

        private static LambdaExpression Deserialize(INode node)
        {
            return provider.GetRequiredService<Serialization.ISerializer>().Deserialize(node) as LambdaExpression;
        }

        private static IServiceProvider GetServiceProvider()
        {
            var services = new ServiceCollection();

            services.AddSingleton<Serialization.ISerializer, Serializer>();
            services.AddSingleton<ITransitionMap, TransitionMap>();

            //register serilizers
            services.AddSingleton<IExpressionSerializer<LambdaExpression>, LambdaExpressionSerializer>();
            services.AddSingleton<IExpressionSerializer<ParameterExpression>, ParameterExpressionSerializer>();
            services.AddSingleton<IExpressionSerializer<BinaryExpression>, BinaryExpressionSerializer>();
            services.AddSingleton<IExpressionSerializer<MemberExpression>, MemberExpressionSerializer>();
            services.AddSingleton<IExpressionSerializer<ConstantExpression>, ConstantExpressionSerializer>();

            //register deserilizers
            services.AddSingleton<IExpressionDeserializer<LambdaNode>, LambdaExpressionSerializer>();
            services.AddSingleton<IExpressionDeserializer<ParameterNode>, ParameterExpressionSerializer>();
            services.AddSingleton<IExpressionDeserializer<BinaryNode>, BinaryExpressionSerializer>();
            services.AddSingleton<IExpressionDeserializer<MemberNode>, MemberExpressionSerializer>();
            services.AddSingleton<IExpressionDeserializer<ConstantNode>, ConstantExpressionSerializer>();

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