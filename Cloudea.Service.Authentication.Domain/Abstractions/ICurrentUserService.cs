using Cloudea.Entity.Base.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloudea.Service.Auth.Domain.Abstractions;

/// <summary>
/// 当前登录的用户(scope)
/// </summary>
public interface ICurrentUserService
{
    /// <summary>
    /// 用户是否登录
    /// </summary>
    Task<bool> CheckUserLogin();
    /// <summary>
    /// 用户信息Id
    /// </summary>
    Task<long> GetUserId();
    /// <summary>
    /// 获得用户信息
    /// </summary>
    /// <returns></returns>
    Task<Base_User> GetUserInfo();
}
