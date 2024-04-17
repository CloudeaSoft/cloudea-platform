using Cloudea.Domain.Identity.Entities;

namespace Cloudea.Application.Abstractions;

/// <summary>
/// 当前登录的用户(scope)
/// </summary>
public interface ICurrentUser
{
    /// <summary>
    /// 用户是否登录
    /// </summary>
    Task<bool> CheckUserLoginAsync(CancellationToken cancellationToken = default);
    /// <summary>
    /// 用户信息Id
    /// </summary>
    Task<Guid> GetUserIdAsync(CancellationToken cancellationToken = default);
    /// <summary>
    /// 获得用户信息
    /// </summary>
    /// <returns></returns>
    Task<User?> GetUserInfoAsync(CancellationToken cancellationToken = default);
}
