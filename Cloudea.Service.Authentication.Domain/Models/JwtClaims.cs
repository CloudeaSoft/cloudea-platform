namespace Cloudea.Service.Auth.Domain.Models
{
    public class JwtClaims
    {
        /// <summary>
        /// 用户 信息ID
        /// </summary>
        public const string USER_ID = "User:Id";
        /// <summary>
        /// 用户登录的GUID 用于保证同时只有一个用户登录
        /// </summary>
        public const string USER_LOGIN_GUID = "User:LoginGuid";
        /// <summary>
        /// 用户权限
        /// </summary>
        public const string USER_PERMISSIONS = "User:Permissions";
    }
}