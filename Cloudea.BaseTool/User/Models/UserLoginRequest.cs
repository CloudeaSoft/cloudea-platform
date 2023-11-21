using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloudea.Service.Base.User.Models
{
    /// <summary>
    /// 用户登录
    /// </summary>
    public class UserLoginRequest
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string? UserName { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string? Email { get; set; }
        /// <summary>
        /// 验证码
        /// </summary>
        public string? Vercode { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string? Password { get; set; }
        /// <summary>
        /// 登陆方式
        /// </summary>
        public LoginType LoginType { get; set; }
    }

    public enum LoginType
    {
        UserNamePassword,
        EmailPassword,
        EmailVercode
    }
}
