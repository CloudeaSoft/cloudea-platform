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
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
    public class IdentityController : ApiControllerBase
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
    {
        private readonly UserDbContext _userDbContext;
        private readonly UserDomainService authUserService;
        private readonly VerificationCodeService verificationCodeService;
        private readonly ICurrentUser _currentUser;

#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
        public IdentityController(UserDomainService authUserService, VerificationCodeService verificationCodeService, UserDbContext userDbContext, ICurrentUser currentUser)
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
        {
            this.authUserService = authUserService;
            this.verificationCodeService = verificationCodeService;
            _userDbContext = userDbContext;
            _currentUser = currentUser;
        }

        [HttpPost]
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
        public async Task<IActionResult> RegisterToken(string email, string verCode)
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
        {
            var res = await authUserService.StartRegister(email, verCode);
            if (res.IsFailure()) {
                return BadRequest(res);
            }
            return Ok(res);
        }

        [HttpPost]
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
        public async Task<IActionResult> Register([FromBody] UserRegisterRequest user)
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
        {
            var userRes = await authUserService.Register(user.RegisterToken, user.UserName, user.Password);
            if (userRes.IsFailure()) {
                return BadRequest(userRes);
            }
            var createRes = await _userDbContext.Create(userRes.Data);
            return Ok(createRes);
        }

        [HttpPost]
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
        public async Task<IActionResult> Login([FromBody] UserLoginRequest request)
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
        {
            // 数据校验
            if (request == null) {
                return BadRequest();
            }
#pragma warning disable CS0472 // 由于此类型的值永不等于 "null"，该表达式的结果始终相同
            if (request.LoginType == null) {
                return BadRequest();
            }
#pragma warning restore CS0472 // 由于此类型的值永不等于 "null"，该表达式的结果始终相同

            var tokenRes = await authUserService.Login(request);
            if (tokenRes.IsFailure()) {
                return NotFound();
            }
            return Ok(tokenRes);
        }

        [HttpPut]
#pragma warning disable CS1998 // 异步方法缺少 "await" 运算符，将以同步方式运行
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
        public async Task<IActionResult> Password(string newPassword)
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
#pragma warning restore CS1998 // 异步方法缺少 "await" 运算符，将以同步方式运行
        {
            return Ok();
        }

        [HttpGet]
#pragma warning disable CS1998 // 异步方法缺少 "await" 运算符，将以同步方式运行
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
        public async Task<IActionResult> UserProfile(long userId)
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
#pragma warning restore CS1998 // 异步方法缺少 "await" 运算符，将以同步方式运行
        {
            return Ok();
        }

        [HttpPut]
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
#pragma warning disable CS1998 // 异步方法缺少 "await" 运算符，将以同步方式运行
        public async Task<IActionResult> UserProfile(UserProfile userProfile)
#pragma warning restore CS1998 // 异步方法缺少 "await" 运算符，将以同步方式运行
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
        {
            return Ok();
        }

        [HttpPost]
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
        public async Task<IActionResult> VerificationCode(string email, VerificationCodeType codeType)
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
        {
            var res = await verificationCodeService.SendVerCodeEmail(email, codeType);
            if (res.IsFailure()) {
                return BadRequest(res);
            }
            return Ok(res);
        }

        [HttpGet]
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
        public async Task<IActionResult> SelfInfo()
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
        {
            var info = await _currentUser.GetUserInfoAsync();
            if (info == null) {
                return BadRequest();
            }
            return Ok(info);
        }
    }
}
