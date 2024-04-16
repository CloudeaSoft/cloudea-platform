using Cloudea.Application.Abstractions;
using Cloudea.Application.Identity;
using Cloudea.Application.Identity.Contracts;
using Cloudea.Domain.Common.API;
using Cloudea.Domain.Identity.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cloudea.Web.Controllers
{
    [Authorize]
    public class IdentityController : ApiControllerBase
    {
        private readonly IdentityService _identityService;
        private readonly VerificationCodeService _verificationCodeService;
        private readonly ICurrentUser _currentUser;

        public IdentityController(IdentityService authUserService, VerificationCodeService verificationCodeService, ICurrentUser currentUser)
        {
            _identityService = authUserService;
            _verificationCodeService = verificationCodeService;
            _currentUser = currentUser;
        }

        /// <summary>
        /// 获取注册Token
        /// </summary>
        /// <param name="email"></param>
        /// <param name="verCode"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> RegisterToken(string email, string verCode)
        {
            var res = await _identityService.GetRegisterTokenAsync(email, verCode);
            if (res.IsFailure)
            {
                return HandleFailure(res);
            }
            return Ok(res);
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public new async Task<IActionResult> User([FromBody] UserRegisterRequest user)
        {
            var res = await _identityService.RegisterAsync(user.RegisterToken, user.UserName, user.Password);
            if (res.IsFailure)
            {
                return HandleFailure(res);
            }
            return Ok(res);
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Session([FromBody] UserLoginRequest request)
        {
            // 数据校验
            if (request == null)
            {
                return BadRequest();
            }

            var tokenRes = await _identityService.LoginAsync(request);
            if (tokenRes.IsFailure)
            {
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
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> VerificationCode(string email, VerificationCodeType codeType)
        {
            var res = await _verificationCodeService.SendVerCodeEmail(email, codeType);
            if (res.IsFailure)
            {
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
            if (info == null)
            {
                return BadRequest();
            }
            return Ok(info);
        }

        /// <summary>
        /// 举报用户
        /// </summary>
        /// <param name="request"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Report([FromBody] CreateReportRequest request, CancellationToken c)
        {
            var res = await _identityService.CreateReportAsync(request, c);
            if (res.IsFailure)
            {
                return HandleFailure(res);
            }

            return Ok(res);
        }

        /// <summary>
        /// 获取举报记录
        /// </summary>
        /// <param name="page"></param>
        /// <param name="index"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Report(int page, int index, CancellationToken c)
        {
            var res = await _identityService.GetReportPageAsync(new()
                {
                    PageIndex = page,
                    PageSize = index
                }, c);

            return res.IsSuccess ? Ok(res) : BadRequest(res);
        }
    }
}
