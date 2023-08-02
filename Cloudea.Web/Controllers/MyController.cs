using Cloudea.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyService;
using MySqlX.XDevAPI.Common;

namespace Cloudea.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MyController :ControllerBase
    {
        private readonly My1Service service;
        public MyController(My1Service service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<int> Reee()
        {
            return service.Send();
        }

    }

    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
