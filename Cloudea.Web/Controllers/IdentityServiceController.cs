using Microsoft.AspNetCore.Mvc;
using Cloudea.Infrastructure.API;
using Cloudea.Entity.Base.User;
using Cloudea.Service.Auth.Domain.User.Models;

namespace Cloudea.Web.Controllers
{
    public class IdentityServiceController : ApiControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> RegisterToken(string email, string verCode)
        {
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] UserRegisterRequest user)
        {
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest request)
        {
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Login()
        {
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Password(string newPassword)
        {
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> UserProfile(long userId)
        {
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Profile(UserProfile userProfile)
        {
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> VerificationCode(string email, VerificationCodeType codeType)
        {
            return Ok();
        }
    }
}
