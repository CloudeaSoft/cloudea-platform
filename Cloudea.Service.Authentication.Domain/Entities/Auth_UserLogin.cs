using Cloudea.Infrastructure.Database;
using FreeSql.DataAnnotations;
using System.ComponentModel.DataAnnotations;

namespace Cloudea.Service.Auth.Domain.Entities;

/// <summary>
/// 用户登录
/// </summary>
[AutoGenerateTable]
public class Auth_UserLogin
{
    /// <summary>
    /// 主键ID
    /// </summary>
    [Column(IsIdentity = true, IsPrimary = true)]
    public long Id { get; set; }

    /// <summary>
    /// 日期 (2021-10-25) DateTime.ToString("yyyy-MM-dd")
    /// </summary>
    [StringLength(10)]
    public string Date { get; set; }
    /// <summary>
    /// 小时  1 2 3 ..  23 DateTime.Now.Hour.ToString();
    /// </summary>
    [StringLength(2)]
    public string Hour { get; set; }
    /// <summary>
    /// 用户id
    /// </summary>
    public long UserId { get; set; }
}
