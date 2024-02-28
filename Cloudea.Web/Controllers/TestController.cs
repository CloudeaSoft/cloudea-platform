using Cloudea.Infrastructure.API;
using Cloudea.Infrastructure.Shared;
using Cloudea.MyService;
using Cloudea.Service.Auth.Domain.Attributes;
using Cloudea.Service.Auth.Domain.Entities;
using Cloudea.Service.Base.Message;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Cloudea.Web.Controllers
{
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
    public class TestController : ApiControllerBase
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
    {
        private readonly TestService testService;
        private readonly ISender _sender;
        private readonly JwtBearerOptions jwtBearerOptions;

#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
        public TestController(TestService testService, ISender sender, IOptions<JwtBearerOptions> jwtBearerOptions)
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
        {
            this.testService = testService;
            _sender = sender;
            this.jwtBearerOptions = jwtBearerOptions.Value;
        }

        /// <summary>
        /// 求和
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        [HttpGet("{a}/{b}")]
        public ActionResult<int> Add(int a, int b) {
            return Ok(a + b);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HasPermission(Permission.AccessMember)]
        [HttpGet]
        public Result Send() {
            return testService.Send();
        }

        /// <summary>
        /// 测试Logger生效情况
        /// </summary>
        [HttpGet]
        public void LoggerTest() {
            testService.LoggerTest();
        }

        /// <summary>
        /// put
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        public string TestPut() {
            return "put";
        }

        [HttpPost]
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
        public async Task<IActionResult> MailTest()
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
        {
            var res = await _sender.Send(new SendEmailRequest() {
                To = new() { "1837622674@qq.com" },
                Subject = "Test",
                Body = "Test"
            });
            return Ok(res);
        }

        [HttpGet]
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
#pragma warning disable CS1998 // 异步方法缺少 "await" 运算符，将以同步方式运行
        public async Task<IActionResult> TestOption()
#pragma warning restore CS1998 // 异步方法缺少 "await" 运算符，将以同步方式运行
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
        {
            return Ok(jwtBearerOptions);
        }

        [HttpGet("Throw")]
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
        public IActionResult Throw() =>
            throw new Exception("Sample exception");
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释

        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("/error")]
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
        public IActionResult HandleError() =>
            Problem();
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释

        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("/error-development")]
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
        public IActionResult HandleErrorDevelopment(
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
            [FromServices] IHostEnvironment hostEnvironment) {
            if (!hostEnvironment.IsDevelopment()) {
                return NotFound();
            }

            var exceptionHandlerFeature =
                HttpContext.Features.Get<IExceptionHandlerFeature>()!;

            return Problem(
                detail: exceptionHandlerFeature.Error.StackTrace,
                title: exceptionHandlerFeature.Error.Message);
        }
    }
}
