using Cloudea.Service.Auth.Domain.Abstractions;
using Cloudea.Service.Auth.Domain.Entities;
using Cloudea.Service.Auth.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Cloudea.Service.Auth.Domain.Applications
{
    public class CurrentUser : ICurrentUser
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserRepository _userService;
        private readonly ILogger<CurrentUser> _logger;

        public CurrentUser(
            IHttpContextAccessor httpContextAccessor,
            IUserRepository userService,
            ILogger<CurrentUser> logger)
        {
            _httpContextAccessor = httpContextAccessor;
            _userService = userService;
            _logger = logger;
        }

        /// <summary>
        /// 用户信息
        /// </summary>
        private User? _user = null;
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
            var claim = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(t => t.Type == JwtClaims.USER_ID);
            if (claim != null) {
                Guid userId = Guid.Parse(claim.Value);
                var res = await _userService.GetUser(userId);
                if (!res.Status) {
                    string err = $"userId:{userId} GetInfo:{res.Message}";
                    _logger.LogError(err);
                    _user = null;
                    _inited = true;
                    return;
                }
                _user = res.Data;
            }
            _inited = true;
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <returns></returns>
        public async Task<User?> GetUserInfoAsync()
        {
            if (_inited) {
                return _user;
            }
            await Init();
            return _user;
        }

        /// <summary>
        /// 检查登陆状态
        /// </summary>
        /// <returns></returns>
        public async Task<bool> CheckUserLoginAsync()
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
        public async Task<Guid> GetUserIdAsync()
        {
            if (_inited) {
                return _user != null ? _user.Id : Guid.Empty;
            }
            await Init();
            return _user != null ? _user.Id : Guid.Empty;
        }
    }
}
