using Cloudea.Entity.Base.User;
using Cloudea.Infrastructure.API;
using Cloudea.Service.Auth.Domain.User;
using Cloudea.Service.Auth.Domain.User.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cloudea.Web.Controllers.Base
{
    public class UserController : ApiControllerBase
    {
        private readonly UserService userService;
        private readonly AuthUserService authUserService;
        private readonly VerificationCodeService verificationCodeService;

        public UserController(UserService userService, AuthUserService authUserService, VerificationCodeService verificationCodeService)
        {
            this.userService = userService;
            this.authUserService = authUserService;
            this.verificationCodeService = verificationCodeService;
        }

        /// <summary>
        /// 获取注册token
        /// </summary>
        /// <param name="email"></param>
        /// <param name="verCode"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> RegisterToken(string email, string verCode)
        {
            var res = await authUserService.StartRegister(email, verCode);
            if (res.IsFailure()) {
                return BadRequest(res);
            }
            return Ok(res);
        }

        /// <summary>
        /// 提交注册信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] UserRegisterRequest user)
        {
            var res = await authUserService.FinishRegister(user);
            if (res.IsFailure()) {
                return BadRequest(res);
            }
            return Ok();
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest request)
        {
            var tokenRes = await authUserService.Login(request);
            if (tokenRes.IsFailure()) {
                return NotFound();
            }
            return Ok(tokenRes.Data);
        }

        /// <summary>
        /// 退出登录
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> Login()
        {
            return Ok();
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Password(string newPassword)
        {
            int userId = 1;
            var res = await userService.SetPassword(userId, newPassword);
            if (res.IsFailure()) {
                return NotFound(res);
            }
            return Ok(res);
        }

        [HttpGet]
        public async Task<IActionResult> UserProfile(long userId)
        {
            var res = await userService.ReadUserById(userId);
            return Ok(res);
        }

        /// <summary>
        /// 修改个人档案
        /// </summary>
        /// <param name="userProfile"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Profile(UserProfile userProfile)
        {
            var res = await userService.UpdateUserProfile(userProfile);
            if (res.IsFailure()) {
                return NotFound("登陆失败");
            }
            return Ok(res);
        }

        /// <summary>
        /// 发送验证码
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> VerificationCode(string email, VerificationCodeType codeType)
        {
            var res = await verificationCodeService.SendVerCodeEmail(email, codeType);
            if (res.IsFailure()) {
                return BadRequest(res);
            }
            return Ok(res);
        }
    }
}
