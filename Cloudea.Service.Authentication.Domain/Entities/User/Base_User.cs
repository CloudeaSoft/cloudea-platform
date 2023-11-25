using Cloudea.Infrastructure.Database;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Cloudea.Entity.Base.User
{
    /// <summary>
    /// 用户信息
    /// </summary>
    [AutoGenerateTable]
    public class Base_User : BaseEntity
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [StringLength(100)]
        public string UserName { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        [StringLength(100)]
        public string NickName { get; set; } = "";
        /// <summary>
        /// 邮箱
        /// </summary>
        [StringLength(100)]
        [EmailAddress]
        [JsonIgnore]
        public string Email { get; set; }
        /// <summary>
        /// 密码 (加密后)
        /// </summary>
        [StringLength(255)]
        [JsonIgnore]
        public string Password { get; set; }
        /// <summary>
        /// 密码混淆
        /// </summary>
        [StringLength(255)]
        [JsonIgnore]
        public string Salt { get; set; } = default!;

        /// <summary>
        /// 头像数据
        /// </summary>
        [StringLength(500)]
        public byte[]? Avatar { get; set; } = null;

        /// <summary>
        /// 是否启用
        /// </summary>
        [JsonIgnore]
        public bool Enable { get; set; } = false;

        /// <summary>
        /// 删除标记
        /// </summary>
        [JsonIgnore]
        public bool DeleteMark { get; set; } = true;
    }
}
