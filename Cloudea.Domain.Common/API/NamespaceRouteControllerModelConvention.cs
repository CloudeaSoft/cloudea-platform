﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloudea.Domain.Common.API
{
    /// <summary>
    /// 命名空间路由
    /// </summary>
    public class NamespaceRouteControllerModelConvention : IControllerModelConvention
    {
        /// <summary>
        /// 路径前缀
        /// </summary>
        private readonly string _prefix;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="prefix"></param>
        public NamespaceRouteControllerModelConvention(string prefix)
        {
            _prefix = prefix;
        }

        /// <summary>
        /// 添加Attribute
        /// </summary>
        /// <param name="controller"></param>
        public void Apply(ControllerModel controller)
        {
            //判断是否是ApiConventionController的派生控制器
            if (controller.ControllerType.BaseType != typeof(NamespaceRouteControllerBase)) {
                return;
            }
            //判断是否有自定义Route特性
            if (controller.ControllerType.GetCustomAttributes(typeof(RouteAttribute), false).Length > 0) {
                return;
            }
            string controllerNamespace = controller.ControllerType.Namespace;
            string temp = "Controllers.";
            int index = controllerNamespace.IndexOf(temp);
            string prefix = _prefix.Trim('/');
            if (index > -1) {
                prefix += "/" + controllerNamespace.Substring(index + temp.Length);
            }
            if (string.IsNullOrWhiteSpace(prefix)) {
                return;
            }
            if (!string.IsNullOrWhiteSpace(prefix)) {
                prefix = prefix.Replace(".", "/");
            }

            foreach (var selector in controller.Selectors.Where(s => s.AttributeRouteModel != null)) {
                selector.AttributeRouteModel.Template = prefix + "/" + selector.AttributeRouteModel.Template;
            }
        }
    }
}
