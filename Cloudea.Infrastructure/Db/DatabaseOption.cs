using FreeSql;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloudea.Infrastructure.Db
{
    public class DatabaseOption
    {
        /// <summary>
        /// 数据库类型
        /// </summary>
        public DataType Type { get; set; }

        /// <summary>
        /// 链接字符串
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// 全局设定
        /// </summary>
        public Action<IFreeSql> GlobalSetting { get; set; }

        /// <summary>
        /// 解析连接字符串
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="dbName"></param>
        /// <returns></returns>
        public static DatabaseOption Parse(string dbName, IConfiguration configuration = null)
        {

            /*string type = configuration[$"ConnectionStrings:{dbName}:Type"];
            string connStr = configuration[$"ConnectionStrings:{dbName}:ConnectionString"];*/
            string type = "MySql";
            string connStr = "server=localhost;port=3306;database=test;user=root;password=123456";
            
            //确认type是否正确
            if (Enum.TryParse<DataType>(type, out DataType dataType))
            {
                return new DatabaseOption()
                {
                    Type = dataType,
                    ConnectionString = connStr
                };
            }
            else
            {
                throw new Exception("错误类型:" + dbName);
            }
        }
    }
}
