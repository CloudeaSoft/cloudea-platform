﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cloudea.Domain.Common.API
{
    /// <summary>
    /// 继承此控制器后，会自动根据命名空间结构修改Route增加前缀
    /// 例如LessSharp.WebApi.Controllers.Sys.A.B命名空间下的UserController控制器，就会生成Sys/A/B/User这样的路由
    /// 如果派生控制器有自定义Route特性的话，就不会自动增加前缀
    /// </summary>
    [Route("[controller]/[action]")]
    [ApiController]
    public abstract class NamespaceRouteControllerBase : ControllerBase
    {

    }
}
