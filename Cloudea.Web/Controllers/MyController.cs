using Cloudea.Core;
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
        private readonly MyService.MyService service;
        public MyController(MyService.MyService service)
        {
            this.service = service;
        }

        [HttpGet("{a}/{b}")]
        public ActionResult<int> Add(int a, int b)
        {

            return Ok(a + b);
        }

        [HttpGet]
        public async Task<string> HowAreYou()
        {
            
            return $"你好,{service.Send()}";
        }
    }

}
