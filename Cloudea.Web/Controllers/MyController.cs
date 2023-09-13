using Cloudea.Core;
using Cloudea.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using MyService;
using MySqlX.XDevAPI.Common;
using Newtonsoft.Json;
using System.Net;

namespace Cloudea.Web.Controllers
{
    public class MyController : ApiControllerBase
    {
        private readonly My1Service service;
        public MyController(My1Service service)
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
