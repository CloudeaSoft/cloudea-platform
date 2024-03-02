using Cloudea.Application.Identity;
using Cloudea.Infrastructure.API;
using Cloudea.Service.Auth.Domain.Abstractions;
using Cloudea.Service.Auth.Domain.Entities;
using Cloudea.Service.Auth.Domain.Models;
using Cloudea.Service.Auth.Domain.Utils;
using Microsoft.AspNetCore.Mvc;

namespace Cloudea.Web.Controllers
{
    public class IdentityController : ApiControllerBase
    {
        private readonly UserService authUserService;
        private readonly VerificationCodeService verificationCodeService;
        private readonly ICurrentUser _currentUser;

        public IdentityController(UserService authUserService, VerificationCodeService verificationCodeService, ICurrentUser currentUser)
        {
            this.authUserService = authUserService;
            this.verificationCodeService = verificationCodeService;
            _currentUser = currentUser;
        }

        /// <summary>
        /// 获取注册Token
        /// </summary>
        /// <param name="email"></param>
        /// <param name="verCode"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> RegisterToken(string email, string verCode)
        {
            var res = await authUserService.StartRegister(email, verCode);
            if (res.IsFailure) {
                return HandleFailure(res);
            }
            return Ok(res);
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> User([FromBody] UserRegisterRequest user)
        {
            var res = await authUserService.RegisterAsync(user.RegisterToken, user.UserName, user.Password);
            if (res.IsFailure) {
                return HandleFailure(res);
            }
            return Ok(res);
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Session([FromBody] UserLoginRequest request)
        {
            // 数据校验
            if (request == null) {
                return BadRequest();
            }

            if (request.LoginType == null) {
                return BadRequest();
            }

            var tokenRes = await authUserService.Login(request);
            if (tokenRes.IsFailure) {
                return NotFound();
            }
            return Ok(tokenRes);
        }

        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <param name="email"></param>
        /// <param name="codeType"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> VerificationCode(string email, VerificationCodeType codeType)
        {
            var res = await verificationCodeService.SendVerCodeEmail(email, codeType);
            if (res.IsFailure) {
                return BadRequest(res);
            }
            return Ok(res);
        }

        /// <summary>
        /// 获取当前账户信息
        /// </summary>
        /// <returns></returns>
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
