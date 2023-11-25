using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Cloudea.Infrastructure
{
    public static class ModuleInitializerExtensions
    {
        /// <summary>
        /// 每个项目中都可以自己写一些实现了IModuleInitializer接口的类，在其中注册自己需要的服务，这样避免所有内容到入口项目中注册
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assemblies"></param>
        public static IServiceCollection RunModuleInitializers(
            this IServiceCollection services
            //IEnumerable<Assembly> assemblies
            )
        {
             var assemblies = GetAllReferencedAssemblies();

            //遍历给定的 Assembly集合
            foreach (var asm in assemblies)
            {
                //获取 assembly 的类型
                Type[] types = asm.GetTypes();
                //筛选 实现了 IModuleInitializer 接口的 类
                var moduleInitializerTypes = types.Where(t => !t.IsAbstract && typeof(IModuleInitializer).IsAssignableFrom(t));
                foreach (var implType in moduleInitializerTypes)
                {
                    var initializer = (IModuleInitializer?)Activator.CreateInstance(implType);
                    if (initializer == null)
                    {
                        throw new ApplicationException($"Cannot create ${implType}");
                    }
                    initializer.Initialize(services);
                }
            }
            return services;
        }

        /// <summary>
        /// 获取所有指定命名空间的类
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Assembly> GetAllReferencedAssemblies()
        {
            var path = AppDomain.CurrentDomain.RelativeSearchPath ?? AppDomain.CurrentDomain.BaseDirectory;
            var referencedAssemblies = System.IO.Directory.GetFiles(path, "Cloudea.*.dll").Select(Assembly.LoadFrom).ToList();
            return referencedAssemblies;
        }
    }
}