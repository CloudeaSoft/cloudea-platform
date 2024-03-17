using Cloudea.Domain.Common.Database;
using System.ComponentModel.DataAnnotations;

namespace Cloudea.Domain.Identity.Entities;

/// <summary>
/// 用户登录
/// </summary>
public class UserLogin : BaseDataEntity
{
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
    public Guid UserId { get; set; }
}
