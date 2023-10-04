using Identity.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Crypto;

namespace Identity.Controllers
{
    [Route("Identity/Account/[action]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly RoleManager<AppRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;

        public IdentityController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="username"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Login(string username, string pwd)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (await _userManager.CheckPasswordAsync(user, pwd)) {
                return Ok();
            }
            else {
                return BadRequest();
            }
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IEnumerable<IdentityError>> Create()
        {
            AppUser user = new() {
                UserName = "caixukun"
            };
            var result = await _userManager.CreateAsync(user, "1234567");
            return result.Errors;
        }

        /// <summary>
        /// 发送重置密码验证邮件
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> SendRestPasswordToken(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null) {
                return BadRequest();
            }
            string token = await _userManager.GeneratePasswordResetTokenAsync(user);
            Console.WriteLine($"Token{token}");
            return Ok();
        }

        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="username"></param>
        /// <param name="token"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult> ResetPassword(string username, string token, string newPassword)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null) {
                return BadRequest("用户名不存在");
            }
            var res = await _userManager.ResetPasswordAsync(user, token, newPassword);
            if (res.Succeeded) {
                await _userManager.ResetAccessFailedCountAsync(user);
                return Ok("密码重置成功");
            }
            else {
                await _userManager.AccessFailedAsync(user);
                return BadRequest("重置失败");
            }

        }

        [HttpPatch]
        public async Task<ActionResult> Test()
        {
            if (await _roleManager.RoleExistsAsync("admin") == false) {
                AppRole role = new AppRole { Name = "admin" };
                var res = await _roleManager.CreateAsync(role);
                if (!res.Succeeded) return BadRequest("Create failed");
            }
            AppUser user = await _userManager.FindByNameAsync("abc");
            if (user == null) {

            }
            return Ok();
        }

        [Authorize]
        [HttpGet]
        public void AutorizeTest()
        {

        }

    }
}
