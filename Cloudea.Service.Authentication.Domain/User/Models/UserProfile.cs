using System.ComponentModel.DataAnnotations;

namespace Cloudea.Service.Auth.Domain.User.Models
{
    public class UserProfile
    {
        [Required]
        public long Id { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        [StringLength(100)]
        public string NickName { get; set; }
        /// <summary>
        /// 头像地址
        /// </summary>
        [StringLength(500)]
        public byte[]? Avatar { get; set; }
    }
}
