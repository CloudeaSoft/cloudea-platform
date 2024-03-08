using Cloudea.Domain.Identity.Entities;

namespace Cloudea.Service.Auth.Domain.Abstractions;

/// <summary>
/// 当前登录的用户(scope)
/// </summary>
public interface ICurrentUser
{
    /// <summary>
    /// 用户是否登录
    /// </summary>
    Task<bool> CheckUserLoginAsync();
    /// <summary>
    /// 用户信息Id
    /// </summary>
    Task<Guid> GetUserIdAsync();
    /// <summary>
    /// 获得用户信息
    /// </summary>
    /// <returns></returns>
    Task<User?> GetUserInfoAsync();
}
