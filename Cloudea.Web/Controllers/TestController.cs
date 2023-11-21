using Cloudea.Infrastructure.Models;
using Cloudea.MyService;
using Microsoft.AspNetCore.Mvc;
using Cloudea.Web.Utils.ApiBase;
using Cloudea.Service.Base.User;
using MediatR;
using Cloudea.Service.Base.Message;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Cloudea.Service.Base.Authentication;
using Cloudea.Entity.Base.Role;

namespace Cloudea.Web.Controllers
{
    public class TestController : NamespaceRouteControllerBase
    {
        private readonly TestService testService;

        private readonly CurrentUserService currentUserService;
        private readonly ISender _sender;
        private readonly JwtBearerOptions jwtBearerOptions;

        public TestController(TestService testService, CurrentUserService currentUserService, ISender sender, IOptions<JwtBearerOptions> jwtBearerOptions)
        {
            this.testService = testService;
            this.currentUserService = currentUserService;
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
        [HasPermission(Base_Permission.AccessMember)]
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

        /// <summary>
        /// 
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Testuser()
        {
            var res = await currentUserService.GetUserInfo();
            if (res is null) {
                return NotFound();
            }
            return Ok(Result.Success(res));
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
    }
}
