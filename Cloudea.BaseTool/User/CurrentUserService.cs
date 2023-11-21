using Cloudea.Entity.Base.User;
using Cloudea.Service.Base.Authentication.Models;
using Cloudea.Service.Base.User;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Cloudea.Service.Base.User
{
    public class CurrentUserService
    {
        private readonly IHttpContextAccessor HttpContextAccessor;

        private readonly UserService UserService;

        private readonly ILogger<CurrentUserService> Logger;

        private readonly AuthUserService AuthUserService;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor, UserService userService, ILogger<CurrentUserService> logger, AuthUserService authUserService)
        {
            HttpContextAccessor = httpContextAccessor;
            UserService = userService;
            Logger = logger;
            AuthUserService = authUserService;
        }

        /// <summary>
        /// 用户信息
        /// </summary>
        private Base_User _user = null;
        /// <summary>
        /// 是否已经完成初始化
        /// </summary>
        private bool _inited = false;
        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns></returns>
        private async Task Init()
        {
            // 从jwt token 中读取登录信息
            var claim = HttpContextAccessor.HttpContext.User.Claims.FirstOrDefault(t => t.Type == JwtClaims.USER_ID);
            if (claim != null) {
                long userId = long.Parse(claim.Value);
                var res = await UserService.Read(userId);
                if (!res.Status) {
                    string err = $"userId:{userId} GetInfo:{res.Message}";
                    Logger.LogError(err);
                    _user = null;
                    _inited = true;
                    return;
                }
                _user = res.Data;
                AuthUserService.RecordLogin(userId);
            }
            _inited = true;
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <returns></returns>
        public async Task<Base_User> GetUserInfo()
        {
            if (_inited) {
                return _user;
            }
            await Init();
            return _user;
        }

        /// <summary>
        /// 检查Token有效性
        /// </summary>
        /// <returns></returns>
        public async Task<bool> CheckUserLogin()
        {
            if (_inited) {
                return _user != null;
            }
            await Init();
            return _user != null;
        }

        /// <summary>
        /// 获取用户Id
        /// </summary>
        /// <returns></returns>
        public async Task<long> GetUserId()
        {
            if (_inited) {
                return _user != null ? _user.Id : 0;
            }
            await Init();
            return _user != null ? _user.Id : 0;
        }
    }
}
