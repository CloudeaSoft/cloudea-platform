using FreeSql;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloudea.Infrastructure.Database
{
    /// <summary>
    /// 数据库设置初始化
    /// </summary>
    public class DatabaseOptions
    {
        /// <summary>
        /// 数据库类型
        /// </summary>
        public DataType Type { get; private set; }

        /// <summary>
        /// 链接字符串
        /// </summary>
        public string ConnectionString { get; private set; }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="databaseName"></param>
        /// <returns></returns>
        public DatabaseOptions(string databaseName, IConfiguration configuration)
        {
            // 读取配置数据
            string? type = configuration[$"ConnectionStrings:{databaseName}:Type"];
            string? connStr = configuration[$"ConnectionStrings:{databaseName}:ConnectionString"];

            // 验证数据合法性
            if (Enum.TryParse(type, out DataType dataType) is false) {
                throw new DatabaseSetupException(databaseName);
            }
            if (connStr is null) {
                throw new DatabaseSetupException(databaseName);
            }

            Type = dataType;
            ConnectionString = connStr;
        }
    }
}
