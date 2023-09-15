using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Security.Principal;

namespace Cloudea.WebTest.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AccountController : ControllerBase
    {
        [HttpGet]
        public async Task<string> RenderHomePageAsync(HttpContext context)
        {
            if(context?.User?.Identity?.IsAuthenticated == true) {
                return "yes";
            }
            else {
                
                return "no";
            }
        }

        [HttpGet]
        public async Task<IActionResult> SignInAsync(HttpContext context)
        {
            var username = "username";
            var password = "password";

            if ("验证成功" != "") {
                var identity = new GenericIdentity(username, password);
                var principal = new ClaimsPrincipal(identity);
                await context.SignInAsync(principal);
            }
            else {
                //携带输入的账号密码 返回登陆界面
            }

            return Ok("SignInPageAsync");
        }

        [HttpGet]
        public async Task<IActionResult> SignOutAsync(HttpContext context)
        {
            await context.SignOutAsync();

            return Ok("SignOutAsync");
        }
    }
}
