using Cloudea.Entity;
using Cloudea.Infrastructure.Db;
using FreeSql;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Cloudea.MiniTest
{
    public class Program
    {
        static void Main(string[] args)
        {
            ServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddDatabase<FirstDb>(
                new FirstDb("server=localhost;port=3306;database=test;user=root;password=123456",
                    FreeSql.DataType.MySql));
            serviceCollection.AddScoped<dbma>();

            using (var sp = serviceCollection.BuildServiceProvider())
            {
                var ppp = sp.GetRequiredService<dbma>();
                ppp.abc();
            }    
        }
    }
}