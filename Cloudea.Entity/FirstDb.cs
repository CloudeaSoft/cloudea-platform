using Cloudea.Infrastructure.Db;
using FreeSql;

namespace Cloudea.Entity
{
    public class FirstDb : DatabaseOption
    {
        /// <summary>
        /// 数据库
        /// </summary>
        /// <param name="connectString"></param>
        public FirstDb(string connectString, DataType type)
        {
            Type = type;
            ConnectionString = connectString;

            GlobalSetting = (fsql) =>
            {
                AddGlobalFilters(fsql);
            };
        }

        /// <summary>
        /// 添加全局过滤器
        /// </summary>
        private void AddGlobalFilters(IFreeSql fsql)
        {
            //添加全局过滤
            //fsql.GlobalFilter.Apply<T>();
        }

        /// <summary>
        /// 用于同步数据库的表
        /// </summary>
        public static List<Type> Tables { get; private set; } = new List<Type>()
        {
            //数据表名
        };
    }
}