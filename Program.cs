using System;
using System.Linq.Expressions;
using System.Runtime.Serialization.Formatters;
using ExpressionsSerialization.Symbols;
using ExpressionsSerialization.Models;
using ExpressionsSerialization.Nodes;
using ExpressionsSerialization.Serialization;
using Newtonsoft.Json;

namespace ExpressionsSerialization
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Test();
            var raw = Prepare();
            Console.WriteLine(raw);
            var check = Deserialize(raw);
            // Console.WriteLine(check(GetUser("Anna", 17)));
            // Console.WriteLine(check(GetUser("Max", 25)));
            // Console.WriteLine(check(GetUser("Alex", 20)));
        }

        private static string Prepare()
        {
            return Serialize(
                user => user.Age >= 18
            );
            // return Serialize(
            //     user => user.Name.StartsWith("A") && (user.Age >= 18 || user.Test)
            // );
            // return Serialize(user => user.Test);
            // return Serialize();
        }

        private static User GetUser(string name, int age)
        {
            return new User
            {
                Name = name,
                Age = age
            };
        }

        private static string Serialize(Expression<Func<User, bool>> expression)
        {
            return JsonConvert.SerializeObject(new ExpressionSerializer().Serialize(expression), new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.Objects,
                TypeNameAssemblyFormat = FormatterAssemblyStyle.Simple
            });
        }

        private static Func<User, bool> Deserialize(string raw)
        {
            var expression = new ExpressionSerializer()
                .Deserialize(
                    JsonConvert.DeserializeObject<Expressions.LambdaExpression>(raw, new JsonSerializerSettings()
                    {
                        TypeNameHandling = TypeNameHandling.Objects
                    })
                ) as Expression<Func<User, bool>>;

            return expression.Compile();
        }

        private static void Test()
        {
            var source = new BinaryNode()
            {
                Left = new ParameterNode()
                {
                    Type = typeof(int),
                    Name = "x"
                },
                Right = new ConstantNode()
                {
                    Value = 5
                }
            };
            var raw = JsonConvert.SerializeObject(source, new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.Objects
            });
            Console.WriteLine(raw);
            var result = JsonConvert.DeserializeObject<BinaryNode>(raw, new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.Objects
            });
        }

        // private static string Prepare()
        // {
        //     return Serialize(
        //         user => user.Age >= 18
        //     );
        //     // return Serialize(
        //     //     user => user.Name.StartsWith("A") && (user.Age >= 18 || user.Test)
        //     // );
        //     // return Serialize(user => user.Test);
        //     // return Serialize();
        // }

        // private static string Serialize(Expression<Func<User, bool>> expression)
        // {
        //     return JsonConvert.SerializeObject(new ExpressionSerializer().Serialize(expression));
        //     // return JsonConvert.SerializeObject(expression);
        //     // var stream = new MemoryStream();

        //     // Serializer.Serialize(stream, expression);

        //     // return Convert.ToBase64String(stream.ToArray());
        // }

        // private static Func<User, bool> Deserialize(string raw)
        // {
        //     var expression = new ExpressionSerializer()
        //         .Deserialize(
        //             (IExpression)JsonConvert.DeserializeObject(raw)
        //         ) as Expression<Func<User, bool>>;

        //     return expression.Compile();
        //     // var stream = new MemoryStream(Convert.FromBase64String(raw));

        //     // var expression = Serializer.Deserialize<Expression<Func<User, bool>>>(stream);

        //     // return expression.Compile();
        // }
    }
}