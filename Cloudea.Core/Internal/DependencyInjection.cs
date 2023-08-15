using Autofac;
using Autofac.Extensions.DependencyInjection;
using Cloudea.Core;
using Cloudea.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Reflection;

namespace Cloudea.Core.Internal
{
    internal static class DependcyInjection
    {
        /// <summary> 
        /// 检查给定Type的LifeTime，仅允许IService的实现类true。
        /// 
        ///     比较type是否与IService相等
        ///         是则判断该类ServiceOptionAttribute是否为空
        ///                 是则判断lifetime的值是否为Transient
        ///                     是则返回true
        ///                     否则返回false
        ///                 否则返回Attr与lifetime比较结果
        ///         否则返回false
        /// </summary>
        /// <param name="type"></param>
        /// <param name="lifeTime"></param>
        /// <returns></returns>
        private static bool CheckComponentLifeTimeEqual(Type type, Lifetime lifeTime)
        {
            ServiceOptionAttribute serviceAttr = type.GetCustomAttribute<ServiceOptionAttribute>();
            if (!typeof(IService).IsAssignableFrom(type))
            {
                return false;
            }
            if (serviceAttr == null)
            {
                if (lifeTime == Lifetime.Transient)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return serviceAttr.Lifetime == lifeTime;
        }

        /// <summary>
        /// 注册Assembly[]中，LifeTime为给定值的Assembly，到ContainerBuilder。 
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="assemblies"></param>
        /// <param name="lifetime"></param>
        private static void RegisterLifetime(ContainerBuilder builder, Assembly[] assemblies, Lifetime lifetime)
        {
            var regInterface = builder
                .RegisterAssemblyTypes(assemblies)
                .Where((t) => CheckComponentLifeTimeEqual(t, lifetime))
                .AsImplementedInterfaces();
            switch (lifetime)
            {
                case Lifetime.Scoped:
                    regInterface = regInterface.InstancePerLifetimeScope();
                    break;

                case Lifetime.Singleton:
                    regInterface = regInterface.SingleInstance();
                    break;

                case Lifetime.Transient:
                    regInterface = regInterface.InstancePerDependency();
                    break;

                default:
                    regInterface = regInterface.InstancePerDependency();
                    break;
            }
            regInterface.PropertiesAutowired();

            var regImpl = builder
    .RegisterAssemblyTypes(assemblies)
    .Where((t) => CheckComponentLifeTimeEqual(t, lifetime));
            switch (lifetime)
            {
                case Lifetime.Scoped:
                    regImpl = regImpl.InstancePerLifetimeScope();
                    break;

                case Lifetime.Singleton:
                    regImpl = regImpl.SingleInstance();
                    break;

                case Lifetime.Transient:
                    regImpl = regImpl.InstancePerDependency();
                    break;

                default:
                    regImpl = regImpl.InstancePerDependency();
                    break;
            }

            regImpl.PropertiesAutowired();
        }

        /// <summary>
        /// 注册此应用程序域的Assembly[]中的Assembly到ContainerBuilder
        /// </summary>
        /// <param name="builder"></param>
        private static void RegisterComponent(ContainerBuilder builder)
        {
            //此应用程序域中的 Assembly[]-程序集的数组。
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            //获取IService类的Type
            Type baseType = typeof(IService);

            //按照3种LifeTime，分别注册
            RegisterLifetime(builder, assemblies, Lifetime.Singleton);
            RegisterLifetime(builder, assemblies, Lifetime.Scoped);
            RegisterLifetime(builder, assemblies, Lifetime.Transient);

            //获取ControllerBase类的Type
            Type controllerBaseType = typeof(ControllerBase);

            //注册ControllerBase的子类到ContainerBuilder，自动完成装配
            builder.RegisterAssemblyTypes(AssemblyLoader.GetAllAssemblies())
                .Where(t => controllerBaseType.IsAssignableFrom(t) && t != controllerBaseType)
                .PropertiesAutowired();
        }

        /// <summary>
        /// 把程序域中所有IService的实现类，注册到IHostBuilder中
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        internal static IHostBuilder Create(IHostBuilder builder)
        {
            return builder.UseServiceProviderFactory(new AutofacServiceProviderFactory(RegisterComponent));
        }
    }
}