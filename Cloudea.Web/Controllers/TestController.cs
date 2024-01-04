using Cloudea.Infrastructure.Models;
using Cloudea.MyService;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Cloudea.Service.Base.Message;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Diagnostics;
using Cloudea.Infrastructure.API;
using Cloudea.Service.Auth.Domain.Abstractions;
using Cloudea.Service.Auth.Domain.Attributes;
using Cloudea.Service.Auth.Domain.Entities;

namespace Cloudea.Web.Controllers
{
    public class TestController : ApiControllerBase
    {
        private readonly TestService testService;
        private readonly ISender _sender;
        private readonly JwtBearerOptions jwtBearerOptions;

        public TestController(TestService testService, ISender sender, IOptions<JwtBearerOptions> jwtBearerOptions)
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
        public ActionResult<int> Add(int a, int b)
        {
            return Ok(a + b);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HasPermission(Permission.AccessMember)]
        [HttpGet]
        public Result Send()
        {
            return testService.Send();
        }

        /// <summary>
        /// 测试Logger生效情况
        /// </summary>
        [HttpGet]
        public void LoggerTest()
        {
            testService.LoggerTest();
        }

        /// <summary>
        /// put
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        public string TestPut()
        {
            return "put";
        }

        [HttpPost]
        public async Task<IActionResult> MailTest()
        {
            var res = await _sender.Send(new SendEmailRequest() {
                To = new() { "1837622674@qq.com" },
                Subject = "Test",
                Body = "Test"
            });
            return Ok(res);
        }

        [HttpGet]
        public async Task<IActionResult> TestOption()
        {
            return Ok(jwtBearerOptions);
        }

        [HttpGet("Throw")]
        public IActionResult Throw() =>
            throw new Exception("Sample exception");

        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("/error")]
        public IActionResult HandleError() =>
            Problem();

        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("/error-development")]
        public IActionResult HandleErrorDevelopment(
            [FromServices] IHostEnvironment hostEnvironment)
        {
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
