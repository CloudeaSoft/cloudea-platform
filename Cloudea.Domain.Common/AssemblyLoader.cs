﻿using System;
using System.Linq;
using System.Reflection;

namespace Cloudea.Domain.Common
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
            var referencedAssemblies = Directory.GetFiles(path, "Cloudea.*.dll").Select(Assembly.LoadFrom).ToArray();
            return referencedAssemblies;
        }
    }
}