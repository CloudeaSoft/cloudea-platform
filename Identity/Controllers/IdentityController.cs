using Identity.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet]
        public async Task<ActionResult> Login(string user, string pwd)
        {
            string user1 = user;
            string pwd1 = pwd;
            var user2 = await _userManager.FindByNameAsync(user1);
            if(await _userManager.CheckPasswordAsync(user2, pwd)) {
                return Ok();
            }
            else {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<IEnumerable<IdentityError>> Create()
        {
            AppUser user = new() {
                UserName = "caixukun"
            };
            var result = await _userManager.CreateAsync(user, "1234567");
            return result.Errors;
        }

        [Authorize]
        [HttpGet]
        public void AccessDenied()
        {

        }
    }
}
