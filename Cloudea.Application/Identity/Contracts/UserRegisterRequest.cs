﻿namespace Cloudea.Application.Identity.Contracts
{
    /// <summary>
    /// 注册账号
    /// </summary>
    public class UserRegisterRequest
    {
        /// <summary>
        /// 注册用的临时token
        /// </summary>
        public string RegisterToken { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
    }
}
