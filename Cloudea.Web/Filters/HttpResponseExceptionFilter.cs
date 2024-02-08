using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Cloudea.Infrastructure.Models;

namespace Cloudea.Web.Filters;

#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
public class HttpResponseExceptionFilter : IActionFilter, IOrderedFilter
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
{
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
    public int Order => int.MaxValue - 10;
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释

#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
    public void OnActionExecuting(ActionExecutingContext context) { }
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释

#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
    public void OnActionExecuted(ActionExecutedContext context)
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
    {
        if (context.Exception is HttpResponseException httpResponseException) {
            context.Result = new ObjectResult(httpResponseException.Value) {
                StatusCode = httpResponseException.StatusCode
            };

            context.ExceptionHandled = true;
        }
    }
}
