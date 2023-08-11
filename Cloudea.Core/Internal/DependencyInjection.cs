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

        private static void RegisterComponent(ContainerBuilder builder)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            Type baseType = typeof(IService);

            RegisterLifetime(builder, assemblies, Lifetime.Singleton);
            RegisterLifetime(builder, assemblies, Lifetime.Scoped);
            RegisterLifetime(builder, assemblies, Lifetime.Transient);

            Type controllerBaseType = typeof(ControllerBase);

            builder.RegisterAssemblyTypes(AssemblyLoader.GetAllAssemblies())
                .Where(t => controllerBaseType.IsAssignableFrom(t) && t != controllerBaseType)
                .PropertiesAutowired();
        }

        internal static IHostBuilder Create(IHostBuilder builder)
        {
            return builder.UseServiceProviderFactory(new AutofacServiceProviderFactory(RegisterComponent));
        }
    }
}