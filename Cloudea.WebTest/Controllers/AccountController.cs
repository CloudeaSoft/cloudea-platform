using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Security.Principal;

namespace Cloudea.WebTest.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    [Authorize]
    public class AccountController : ControllerBase
    {
        [HttpGet]
        [AllowAnonymous]
        public string RenderHomePageAsync()
        {
            return "";
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult LoginAsync()
        {
            return Ok("SignInPageAsync");
        }

        [HttpGet]
        public ActionResult LogoutAsync()
        {
            return Ok("SignOutAsync");
        }
    }
}
