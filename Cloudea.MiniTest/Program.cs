using Cloudea.Entity;
using Cloudea.Infrastructure.Db;
using Cloudea.Infrastructure.Models;
using FreeSql;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text;

namespace Cloudea.MiniTest
{
    public class Program
    {
        static void Main(string[] args)
        {
            //ServiceCollection serviceCollection = new ServiceCollection();
            //serviceCollection.AddDatabase<FirstDb>(
            //    new FirstDb("server=localhost;port=3306;database=test;user=root;password=123456",
            //        FreeSql.DataType.MySql));
            //serviceCollection.AddScoped<dbma>();

            //using (var sp = serviceCollection.BuildServiceProvider())
            //{
            //    var ppp = sp.GetRequiredService<dbma>();
            //    ppp.abc();
            //}    

            /*var a = Result.Success("abc");

            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("Data: " + a.Data);
            Console.WriteLine("Status: " + a.Status);
            Console.WriteLine("Message: " + a.Message);
            Console.Read();*/

            var p = new Program();

            int a = 4;
            int b = p.add(out a);

            int c = 4;
            int d = p.addd(c);

            Console.WriteLine(b);
            Console.WriteLine(c);
        }

        public int add(out int num)
        {
            num = 0;
            return num;
        }

        public int addd(int num)
        {
            num = 0;
            return num;
        }
    }
}