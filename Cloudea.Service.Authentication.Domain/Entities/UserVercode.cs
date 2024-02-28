using Cloudea.Infrastructure.Database;
using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Cloudea.Service.Auth.Domain.Entities
{
    /// <summary>
    /// 验证码
    /// </summary>
    [AutoGenerateTable]
    [Table(Name = "auth_user_vercode")]
    public class UserVercode : BaseEntity
    {
        /// <summary>
        /// 邮箱
        /// </summary>
        [StringLength(50)]
        public string Email { get; set; }

        /// <summary>
        /// 验证码
        /// </summary>
        [StringLength(50)]
        [JsonIgnore]
        public string VerCode { get; set; }

        /// <summary>
        /// 验证码过期时间
        /// </summary>
        [JsonIgnore]
        public DateTime? VerCodeVaildTime { get; set; }
        /// <summary>
        /// 验证码类型
        /// </summary>
        [JsonIgnore]
        public VerificationCodeType VerCodeType { get; set; }
    }


    /// <summary>
    /// 账号 验证码类型
    /// </summary>
    public enum VerificationCodeType
    {
        /// <summary>
        /// 通过邮箱登录
        /// </summary>
        LoginByEmail = 0,
        /// <summary>
        /// 通过邮箱注册
        /// </summary>
        RegisterByEmail = 1,
        /// <summary>
        /// 通过手机重置密码
        /// </summary>
        ResetPasswordByEmail = 2,
        /// <summary>
        /// 检验手机号有效
        /// </summary>
        CheckMobilePhoneValid = 3
    }

}
