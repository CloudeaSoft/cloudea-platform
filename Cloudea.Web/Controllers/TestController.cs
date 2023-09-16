using Cloudea.Infrastructure.Models;
using Cloudea.MyService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net;
using Cloudea.Infrastructure;
using Cloudea.Web.Utils.ApiBase;

namespace Cloudea.Web.Controllers
{
    public class TestController : NamespaceRouteControllerBase
    {
        private readonly TestService testService;

        public TestController(TestService testService)
        {
            this.testService = testService;
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
    }

}
