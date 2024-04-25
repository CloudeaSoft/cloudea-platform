using Cloudea.Application.Abstractions;
using Cloudea.Domain.Identity.Entities;
using Cloudea.Domain.Identity.Models;
using Cloudea.Domain.Identity.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Cloudea.Application.Identity
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
        private async Task Init(CancellationToken cancellationToken = default)
        {
            // 从jwt token 中读取登录信息
            var claim = _httpContextAccessor.HttpContext!.User.Claims.FirstOrDefault(t => t.Type == JwtClaims.USER_ID);
            if (claim != null)
            {
                Guid userId = Guid.Parse(claim.Value);
                var res = await _userService.GetByIdAsync(userId, cancellationToken);
                if (res is null)
                {
                    string err = $"userId:{userId} GetInfo:User.NotFound";
                    _logger.LogError(err);
                    _user = null;
                    _inited = true;
                    return;
                }
                _user = res;
            }
            _inited = true;
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <returns></returns>
        public async Task<User?> GetUserInfoAsync(CancellationToken cancellationToken = default)
        {
            if (_inited)
            {
                return _user;
            }
            await Init(cancellationToken);
            return _user;
        }

        /// <summary>
        /// 检查登陆状态
        /// </summary>
        /// <returns></returns>
        public async Task<bool> CheckUserLoginAsync(CancellationToken cancellationToken = default)
        {
            if (_inited)
            {
                return _user is not null;
            }
            await Init(cancellationToken);
            return _user is not null;
        }

        /// <summary>
        /// 获取用户Id
        /// </summary>
        /// <returns></returns>
        public async Task<Guid> GetUserIdAsync(CancellationToken cancellationToken = default)
        {
            if (_inited)
            {
                return _user is not null ? _user.Id : Guid.Empty;
            }
            await Init(cancellationToken);
            return _user is not null ? _user.Id : Guid.Empty;
        }
    }
}
