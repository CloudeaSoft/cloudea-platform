using Cloudea.Infrastructure;
using Cloudea.Infrastructure.API;
using Cloudea.Infrastructure.Database;
using Cloudea.Infrastructure.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cloudea.Web.Controllers {
    /*[Authorize]*/
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
    public class DevController : ApiControllerBase
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
    {
        private IFreeSql Database { get; set; }
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
        public ILogger<DevController> Logger { get; set; }
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释

#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
        public DevController(IFreeSql database, ILogger<DevController> logger)
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
        {
            Database = database;
            Logger = logger;
        }

        /// <summary>
        /// 登录运维
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login(string password) {
            if (password != "jst@123456") {
                return Unauthorized("密码错误");
            }

            HttpContext.Response.Cookies.Append("Dev", EncryptionUtils.HMACSHA256("Logged," + DateTime.Now.ToString("yyyy-MM-dd"), "jstsha256secret"), new CookieOptions() {
                Expires = DateTimeOffset.MaxValue
            });

            return Ok("完成登录");
        }

        /// <summary>
        /// 同步数据库表结构
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult SyncDatabase() {

            try {
                // 扫描所有dll
                var typesList = AssemblyLoader.GetAllAssemblies().Select(t => t.GetTypes()).ToList();
                List<Type> entityTypes = new List<Type>();

                foreach (var types in typesList) {

                    foreach (var type in types) {
                        if (type.CustomAttributes.Where(t => t.AttributeType == typeof(AutoGenerateTableAttribute)).Any()) {
                            entityTypes.Add(type);
                        }
                    }

                }

                Database.SyncStructure(entityTypes);
                return Ok("同步完成");
            }
            catch (Exception ex) {
                Logger.LogError(ex.ToString());
                return BadRequest();
            }
        }
    }
}
