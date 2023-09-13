using Cloudea.Infrastructure.Models;
using Cloudea.MyService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using MySqlX.XDevAPI.Common;
using Newtonsoft.Json;
using System.Net;
using Cloudea.Infrastructure;
using Cloudea.Web.Controllers.Base;

namespace Cloudea.Web.Controllers
{
    public class MyController : ApiControllerBase
    {
        private readonly TestService service;
        public MyController(TestService service)
        {
            this.service = service;
        }

        [HttpGet("{a}/{b}")]
        public ActionResult<int> Add(int a, int b)
        {

            return Ok(a + b);
        }

        [HttpGet]
        public string HowAreYou()
        {
            
            return $"你好,{service.Send()}";
        }
    }

}
