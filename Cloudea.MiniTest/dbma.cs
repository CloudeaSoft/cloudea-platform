using Cloudea.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Cloudea.Infrastructure.Db;

namespace Cloudea.MiniTest
{
    public class dbma
    {
        private readonly IFreeSql Database;
        public dbma(IFreeSql database) 
        { 
            Database = database;
        }

        public async void abc()
        {
            var a = await Database.Select<Student>().FirstAsync();
            await Console.Out.WriteLineAsync(a.Name);
        }
    }
}
