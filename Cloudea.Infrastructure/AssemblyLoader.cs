using System.Reflection;

namespace Cloudea.Infrastructure
{
    public static class AssemblyLoader
    {
        /// <summary>
        /// 获得所有Assembly
        /// </summary>
        /// <returns></returns>
        public static Assembly[] GetAllAssemblies()
        {
            var path = AppDomain.CurrentDomain.RelativeSearchPath ?? AppDomain.CurrentDomain.BaseDirectory;
            var referencedAssemblies = System.IO.Directory.GetFiles(path, "Jst.*.dll").Select(Assembly.LoadFrom).ToArray();
            return referencedAssemblies;
        }
    }
}