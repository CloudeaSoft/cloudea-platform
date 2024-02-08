using Cloudea.Service.Auth.Domain.Applications;
using Cloudea.Service.Auth.Domain.Models;

namespace Cloudea.Web.Middlewares
{
    /// <summary>
    /// 唯一用户登录
    /// </summary>
    public class UserLoginGuidAuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
        public UserLoginGuidAuthenticationMiddleware(RequestDelegate next)
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
        {
            _next = next;
        }

#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
        public async Task InvokeAsync(HttpContext context, UserDomainService authUserService)
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
        {

            var req = context.Request;
            Console.WriteLine(req.Headers.Authorization);

            // 检查 user login guid
            if (req.Headers.TryGetValue("Authorization", out _)) {
                var userIdClaim = context.User.Claims.FirstOrDefault(t => t.Type == JwtClaims.USER_ID);
#pragma warning disable CS8600 // 将 null 字面量或可能为 null 的值转换为非 null 类型。
                string userId = userIdClaim?.Value;
#pragma warning restore CS8600 // 将 null 字面量或可能为 null 的值转换为非 null 类型。

                Console.WriteLine("userid:" + userId);


                var userGuidClaim = context.User.Claims.FirstOrDefault(t => t.Type == JwtClaims.USER_LOGIN_GUID);
#pragma warning disable CS8600 // 将 null 字面量或可能为 null 的值转换为非 null 类型。
                string guid = userGuidClaim?.Value;
#pragma warning restore CS8600 // 将 null 字面量或可能为 null 的值转换为非 null 类型。

                Console.WriteLine("guid:" + guid);

                if (string.IsNullOrWhiteSpace(userId) == false && string.IsNullOrWhiteSpace(guid) == false) {
                    // 两个不为空的时候判断只能有同一个人同时登录
                    var savedGuid = authUserService.GetUserLoginGuid(userId);
                    Console.WriteLine("savedGuid:" + savedGuid);
                    if (string.IsNullOrWhiteSpace(savedGuid)) {
                        // 如果为空 设置当前的guid保存
                        authUserService.SetUserLoginGuid(userId, guid);
                    }
                    else {
                        // 不为空 如果不相等说明不是当前登录的用户 返回409 Conflict
                        if (savedGuid != guid) {
                            context.Response.Headers.Add("CLOUDEA-USER-LOGOUT", "ONE-USER-LIMIT");
                            context.Response.StatusCode = 409;// Conflict
                            return;
                        }
                    }
                }
            }

            await _next(context);
        }
    }
}
