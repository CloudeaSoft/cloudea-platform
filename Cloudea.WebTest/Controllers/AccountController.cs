using Cloudea.Entity.Demo;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Security.Principal;

namespace Cloudea.WebTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly IFreeSql Database;

        IMemoryCache memoryCache;

        public AccountController(IFreeSql database)
        {

            Database = database;
        }

        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Account(AccountBaseInfo accountBaseInfo)
        {
            // 1. 查重
            if (await NowUtils.CheckIfExists(Database, accountBaseInfo.Email, accountBaseInfo.Username)) {
                return BadRequest("用户已存在");
            }
            // 2. 加密密码

            // 3. 插入数据库
            var a = await Database.Insert(new Account_Base(
                accountBaseInfo.Email,
                accountBaseInfo.Username,
                accountBaseInfo.Password
                )).ExecuteAffrowsAsync();
            // 4. 插入失败则
            if (a <= 0) {
                return BadRequest("创建用户失败");
            }
            return Ok();
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> Account()
        {
            var list = await Database.Select<Account_Base>().ToListAsync();
            return Ok(list);
        }

        [HttpPatch]
        [AllowAnonymous]
        public async Task<ActionResult> Account(long id)
        {
            var a = await Database
                .Update<Account_Base>()
                .Where(x => x.Id == id)
                .Set(b => new { Password = "123"})
                .ExecuteAffrowsAsync();

            return Ok(a);
        }

    /*[HttpGet]
    [AllowAnonymous]
    public ActionResult Signin()
    {
        return Ok("SignInPage");
    }

    [HttpGet]
    public ActionResult Signout()
    {
        return Ok("SignOut");
    }*/


}

public class AccountBaseInfo
{
    [EmailAddress]
    public string Email { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
}

public class NowUtils
{
    public static async Task<bool> CheckIfExists(IFreeSql database, String email, String username)
    {
        var emailCheck = await CheckIfExistsEmail(database, email);
        var usernameCheck = await CheckIfExistsUsername(database, username);
        return emailCheck && usernameCheck;
    }

    public static async Task<bool> CheckIfExistsEmail(IFreeSql database, String email)
    {
        bool emailCheck = await database.Select<Account_Base>().Where(x => x.Email == email).AnyAsync();
        return emailCheck;
    }
    public static async Task<bool> CheckIfExistsUsername(IFreeSql database, String username)
    {
        bool usernameCheck = await database.Select<Account_Base>().Where(x => x.Username == username).AnyAsync();
        return usernameCheck;
    }
}
}
