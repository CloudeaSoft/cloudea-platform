using FreeSql;
using FreeSql.Aop;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Cloudea.Infrastructure.Db
{
    public static class Extensions
    {
        public static IServiceCollection AddDatabase<TOption>(this IServiceCollection services,
            DatabaseOption option,
            Action<IFreeSql<TOption>>? optionAction = null // Null
            ) where TOption : DatabaseOption // 泛型约束：继承自DatabaseOption的数据库
        {
            //创建 IFreesql对象
            IFreeSql fsql = new FreeSqlBuilder()
                .UseConnectionString(option.Type, option.ConnectionString)
                .Build<TOption>();

            /*//创建
            PropertyInfo createTimeProp = typeof(EntityBase).GetProperty(nameof(EntityBase.CreateTime));
            var updateTimeProp = typeof(EntityBase).GetProperty(nameof(EntityBase.UpdateTime));

            //添加 自动处理
            fsql.Aop.AuditValue += auditValue;
            fsql.Aop.CommandBefore += Aop_CommandBefore<TOption>;
            
            /// fsql
            if (option != null && option.GlobalSetting != null)
            {
                option.GlobalSetting(fsql);
            }*/

            var type = typeof(IFreeSql);
            services.AddSingleton(type, fsql);
            services.AddSingleton(typeof(TOption));


            //optionAction?.Invoke(fsql);
            return services;
        }

        private static void Aop_CommandBefore<TOption>(object sender, CommandBeforeEventArgs e)
        {

            if (string.IsNullOrEmpty(TransactionControl<TOption>.TransactionId.Value) == false && e.Command.Transaction == null)
            {
                // 附上事务
                var context = TransactionControlPool.Instance.GetContext(TransactionControl<TOption>.TransactionId.Value);
                if (context != null)
                {
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

            if (sqlService == null)
            {
                throw new Exception("默认数据库不存在");
            }
            if (sqlService.ImplementationInstance == null)
            {
                throw new Exception("默认数据库未初始化");
            }

            services.AddSingleton(typeof(IFreeSql), sqlService.ImplementationInstance);
            // 设置默认 获得事务的方法
            FreeSqlTransactionExtendsion.GetDefaultTransaction = (fsql) =>
            {
                var instance = fsql as IFreeSql<TOption>;
                return instance.BeginTransaction();
            };
            return services;
        }

        // 存储 基础实体 的类型
        private static Type _baseEntityType = typeof(EntityBase);
        // 存储 基础实体 的Create和Update属性
        private static PropertyInfo _createProp = typeof(EntityBase).GetProperty(nameof(EntityBase.CreateTime));
        private static PropertyInfo _updateProp = typeof(EntityBase).GetProperty(nameof(EntityBase.UpdateTime));   

        // 插入/更新时统一处理的值(插入时间，修改时间）
        private static void auditValue(object sender, AuditValueEventArgs args)
        {
            // 判断声明该成员的类是否为 _baseEntityType => EntityBase
            if (args.Property.DeclaringType != _baseEntityType)
            {
                return;
            }

            // 插入动作 & 更新动作
            if (args.Property.Name == _createProp.Name && args.AuditValueType == AuditValueType.Insert)
            {
                args.Value = DateTime.Now;
            } 
            else if (args.Property.Name == _updateProp.Name)
            {
                args.Value = DateTime.Now;
            }
        }
    }
}
