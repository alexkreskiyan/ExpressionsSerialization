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

        public static void Main_()
        {
            var param = Expression.Parameter(typeof(int), "x");
            var expression = Expression.Lambda(
                Expression.GreaterThanOrEqual(
                    param,
                    Expression.Constant(5)
                ),
                param
            );

            var func = expression.Compile();
            var result = func.DynamicInvoke(6);
            Console.WriteLine(result);
        }

        public static void Main()
        {
            provider = GetServiceProvider();
            var raw = Serialize(
                (user, age) => user.Age >= age
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
            var answer = action.DynamicInvoke(GetUser("Alex", 18), 19);
            Console.WriteLine(answer);
        }

        private static INode Serialize(Expression<Func<User, int, bool>> expression)
        {
            return provider.GetRequiredService<ISerializer>().Serialize(expression);
        }

        private static LambdaExpression Deserialize(INode node)
        {
            return provider.GetRequiredService<ISerializer>()
                .Deserialize(provider.GetRequiredService<IDeserializationContext>(), node) as LambdaExpression;
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