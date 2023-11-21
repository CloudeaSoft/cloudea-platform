using Cloudea.Infrastructure.Database;
using FreeSql.DataAnnotations;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public string Email { get; set; }
        /// <summary>
        /// 密码 (加密后)
        /// </summary>
        [StringLength(255)]
        [JsonIgnore]
        [JsonProperty]
        public string Password { get; set; }
        /// <summary>
        /// 密码混淆
        /// </summary>
        [StringLength(255)]
        [JsonIgnore]
        [JsonProperty]
        public string Salt { get; set; } = default!;

        /// <summary>
        /// 头像地址
        /// </summary>
        [StringLength(500)]
        public string? Avatar { get; set; } = null;

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Enable { get; set; } = false;

        /// <summary>
        /// 删除标记
        /// </summary>
        public bool DeleteMark { get; set; } = true;
    }
}
