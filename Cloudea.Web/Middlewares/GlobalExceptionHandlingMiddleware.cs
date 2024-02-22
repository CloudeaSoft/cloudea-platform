using Microsoft.AspNetCore.Mvc;
using System.Net;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Cloudea.Web.Middlewares {
    /// <summary>
    /// 全局错误捕获
    /// </summary>
    public class GlobalExceptionHandlingMiddleware : IMiddleware {
        private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
        public GlobalExceptionHandlingMiddleware(ILogger<GlobalExceptionHandlingMiddleware> logger)
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
        {
            _logger = logger;
        }

        /// <summary>
        /// 处理过程
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context, RequestDelegate next) {
            try {
                await next(context);
            }
            catch (Exception ex) {
                await HandleExceptionAsync(context, ex);
            }
        }

        /// <summary>
        /// 记录异常并返回500
        /// </summary>
        /// <param name="context"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        private async Task HandleExceptionAsync(HttpContext context, Exception exception) {
            var response = context.Response;

            response.ContentType = "application/json";

            // 生成错误信息
            ProblemDetails problem;
            switch (exception) {
                case NullReferenceException ex:
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    problem = new() {
                        Status = (int)HttpStatusCode.NotFound,
                        Type = "404 Not Found",
                        Title = "404 Not Found",
                        Detail = "Null"
                    };
                    break;
                default:
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    problem = new() {
                        Status = (int)HttpStatusCode.InternalServerError,
                        Type = "Server error",
                        Title = "Server error",
                        Detail = "An internal server has occured"
                    };
                    break;
            }

            string result = JsonSerializer.Serialize(problem);
            await context.Response.WriteAsync(result);
        }
    }
}
