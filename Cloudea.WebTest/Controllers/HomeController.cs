using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Cloudea.WebTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        readonly IDiagnosticContext _diagnosticContext;

        public HomeController(IDiagnosticContext diagnosticContext)
        {
            _diagnosticContext = diagnosticContext ??
                throw new ArgumentNullException(nameof(diagnosticContext));
        }

        [HttpGet]
        public IActionResult Index()
        {
            var order = new Order() {
                Id = 1,
                Name = "abc",
            };
            if (!IsRight(order))
                return BadRequest();
            return Ok();
        }

        private static bool IsRight(Order? order)
        {
            return order.Id >= 0 ||
                            order.Name == "abc";
        }
    }

    public class Order
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
