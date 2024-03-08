using Cloudea.Application.Identity;
using Cloudea.Service.Auth.Domain.Models;
using System.Security.Claims;

namespace Cloudea.Web.Middlewares
{
    /// <summary>
    /// 唯一用户登录
    /// </summary>
    public class UserLoginGuidAuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        public UserLoginGuidAuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, UserService authUserService)
        {
            var req = context.Request;
            Console.WriteLine(req.Headers.Authorization);

            // 检查 user login guid
            if (req.Headers.TryGetValue("Authorization", out _)) {
                Claim? userIdClaim = context.User.Claims.FirstOrDefault(t => t.Type == JwtClaims.USER_ID);
                string userId = userIdClaim!.Value;

                // Console.WriteLine("userid:" + userId);

                var userGuidClaim = context.User.Claims.FirstOrDefault(t => t.Type == JwtClaims.USER_LOGIN_GUID);
                string guid = userGuidClaim!.Value;

                // Console.WriteLine("guid:" + guid);

                if (string.IsNullOrWhiteSpace(userId) is false && string.IsNullOrWhiteSpace(guid) is false) {
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
                            context.Response.Headers.Append("CLOUDEA-USER-LOGOUT", "ONE-USER-LIMIT");
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
