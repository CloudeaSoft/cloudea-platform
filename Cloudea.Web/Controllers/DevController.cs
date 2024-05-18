using Cloudea.Domain.Common;
using Cloudea.Domain.Common.API;
using Cloudea.Domain.Common.Utils;
using Cloudea.Domain.Identity.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cloudea.Web.Controllers
{
    [HasPermission(Domain.Identity.Entities.Permission.AccessMember)]
    public class DevController : ApiControllerBase
    {
        public ILogger<DevController> Logger { get; set; }

        public DevController( ILogger<DevController> logger)
        {
            Logger = logger;
        }

        /// <summary>
        /// 登录运维
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login(string password)
        {
            if (password != "jst@123456")
            {
                return Unauthorized("密码错误");
            }

            HttpContext.Response.Cookies.Append("Dev", EncryptionUtils.HMACSHA256("Logged," + DateTime.Now.ToString("yyyy-MM-dd"), "jstsha256secret"), new CookieOptions() {
                Expires = DateTimeOffset.MaxValue
            });

            return Ok("完成登录");
        }
    }
}
