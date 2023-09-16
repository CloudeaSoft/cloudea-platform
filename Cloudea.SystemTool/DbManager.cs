using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cloudea.Entity;
using Cloudea.Infrastructure.Db;

namespace Cloudea.SystemTool
{
    public class DbManager
    {
        private readonly IFreeSql Database;

        public DbManager(IFreeSql database)
        {
            Database = database;
        }

        public string SyncDatabaseStructure()
        {
            Database.SyncStructure(new List<Type>() { typeof(Student) });
            return "同步成功";
        }

    }
}