using Cloudea.Infrastructure.Domain;
using FreeSql;
using FreeSql.Aop;
using FreeSql.Internal;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Cloudea.Infrastructure.Database
{
    public static class Extensions
    {

        /// <summary>
        /// FreeSql官方提供的默认注入
        /// </summary>
        /// <param name="services"></param>
        /// <param name="dataType"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static IServiceCollection AddDataBaseDefault(this IServiceCollection services,
            DataType dataType,
            string connectionString
            )
        {
            Func<IServiceProvider, IFreeSql> fsqlFactory = r => {
                IFreeSql fsql = new FreeSql.FreeSqlBuilder()
                    .UseConnectionString(dataType, connectionString)
                    .UseMonitorCommand(cmd => Console.WriteLine($"Sql：{cmd.CommandText}"))//监听SQL语句
                                                                                          //.UseAutoSyncStructure(true) //自动同步实体结构到数据库，FreeSql不会扫描程序集，只有CRUD时才会生成表。
                    .UseMappingPriority(MappingPriorityType.FluentApi, MappingPriorityType.Attribute, MappingPriorityType.Aop)
                    .Build();

                fsql.Aop.AuditValue += AuditValue;
                return fsql;
            };
            services.AddSingleton(fsqlFactory);
            return services;
        }

        private static string _createProp = nameof(IHasCreationTime.CreationTime);
        private static string _updateProp = nameof(IHasModificationTime.ModificationTime);
        /// <summary>
        /// 插入/更新时统一处理的值(插入时间，修改时间）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private static void AuditValue(object sender, AuditValueEventArgs args)
        {
            var propTypeName = args.Property.Name;
            //Console.WriteLine(propTypeName);
            //Console.WriteLine(_createProp);
            //Console.WriteLine(_updateProp);

            // 插入动作
            if (propTypeName == _createProp && args.AuditValueType == AuditValueType.Insert) {
                args.Value = DateTime.Now;
            }
            // 更新动作
            else if (propTypeName == _updateProp && args.AuditValueType == AuditValueType.Update) {
                args.Value = DateTime.Now;
            }
        }

        #region 以下代码不启用
        /// <summary>
        /// 自动任务
        /// </summary>
        /// <typeparam name="TOption"></typeparam>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Aop_CommandBefore<TOption>(object sender, CommandBeforeEventArgs e)
        {

            if (string.IsNullOrEmpty(TransactionControl<TOption>.TransactionId.Value) == false && e.Command.Transaction == null) {
                // 附上事务
                var context = TransactionControlPool.Instance.GetContext(TransactionControl<TOption>.TransactionId.Value);
                if (context != null) {
                    e.Command.Transaction = context.UnitOfWork.GetOrBeginTransaction();
                    e.Command.Connection = e.Command.Transaction.Connection;
                }
            }
        }

        /// <summary>
        /// 设置默认数据库
        /// </summary>
        /// <typeparam name="TOption"></typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection SetDefaultDatabase<TOption>(this IServiceCollection services)
        {
            var sqlService = services.FirstOrDefault(t => t.ServiceType == typeof(IFreeSql<TOption>));

            if (sqlService == null) {
                throw new Exception("默认数据库不存在");
            }
            if (sqlService.ImplementationInstance == null) {
                throw new Exception("默认数据库未初始化");
            }

            services.AddSingleton(typeof(IFreeSql), sqlService.ImplementationInstance);
            // 设置默认 获得事务的方法
            FreeSqlTransactionExtendsion.GetDefaultTransaction = (fsql) => {
                var instance = fsql as IFreeSql<TOption>;
                return instance.BeginTransaction();
            };
            return services;
        }
        #endregion
    }
}
