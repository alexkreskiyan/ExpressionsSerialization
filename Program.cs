using System;
using System.Linq.Expressions;
using ExpressionsSerialization.Models;
using ExpressionsSerialization.Serialization;
using Newtonsoft.Json;

namespace ExpressionsSerialization
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var raw = Prepare();
            Console.WriteLine(raw);
            // var check = Deserialize(raw);
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

        // private static User GetUser(string name, int age)
        // {
        //     return new User
        //     {
        //         Name = name,
        //         Age = age
        //     };
        // }

        private static string Serialize(Expression<Func<User, bool>> expression)
        {
            return JsonConvert.SerializeObject(new ExpressionSerializer().Serialize(expression));
            // return JsonConvert.SerializeObject(expression);
            // var stream = new MemoryStream();

            // Serializer.Serialize(stream, expression);

            // return Convert.ToBase64String(stream.ToArray());
        }

        // private static Func<User, bool> Deserialize(string raw)
        // {
        //     var stream = new MemoryStream(Convert.FromBase64String(raw));

        //     var expression = Serializer.Deserialize<Expression<Func<User, bool>>>(stream);

        //     return expression.Compile();
        // }
    }
}