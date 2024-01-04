using Cloudea.Infrastructure.API;
using Cloudea.Service.Auth.Domain.Abstractions;
using Cloudea.Service.Auth.Domain.Applications;
using Cloudea.Service.Auth.Domain.Entities;
using Cloudea.Service.Auth.Domain.Models;
using Cloudea.Service.Auth.Domain.Utils;
using Cloudea.Service.Auth.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Cloudea.Web.Controllers
{
    public class IdentityController : ApiControllerBase
    {
        private readonly UserDbContext _userDbContext;
        private readonly UserDomainService authUserService;
        private readonly VerificationCodeService verificationCodeService;
        private readonly ICurrentUser _currentUser;

        public IdentityController(UserDomainService authUserService, VerificationCodeService verificationCodeService, UserDbContext userDbContext, ICurrentUser currentUser)
        {
            this.authUserService = authUserService;
            this.verificationCodeService = verificationCodeService;
            _userDbContext = userDbContext;
            _currentUser = currentUser;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterToken(string email, string verCode)
        {
            var res = await authUserService.StartRegister(email, verCode);
            if (res.IsFailure()) {
                return BadRequest(res);
            }
            return Ok(res);
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] UserRegisterRequest user)
        {
            var userRes = await authUserService.Register(user.RegisterToken, user.UserName, user.Password);
            if (userRes.IsFailure()) {
                return BadRequest(userRes);
            }
            var createRes = await _userDbContext.Create(userRes.Data);
            return Ok(createRes);
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest request)
        {
            // 数据校验
            if (request == null) {
                return BadRequest();
            }
            if (request.LoginType == null) {
                return BadRequest();
            }

            var tokenRes = await authUserService.Login(request);
            if (tokenRes.IsFailure()) {
                return NotFound();
            }
            return Ok(tokenRes);
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
        public async Task<IActionResult> UserProfile(UserProfile userProfile)
        {
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> VerificationCode(string email, VerificationCodeType codeType)
        {
            var res = await verificationCodeService.SendVerCodeEmail(email, codeType);
            if (res.IsFailure()) {
                return BadRequest(res);
            }
            return Ok(res);
        }

        [HttpGet]
        public async Task<IActionResult> SelfInfo()
        {
            var info = await _currentUser.GetUserInfoAsync();
            if (info == null) {
                return BadRequest();
            }
            return Ok(info);
        }
    }
}
