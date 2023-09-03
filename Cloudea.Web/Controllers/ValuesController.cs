using Cloudea.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cloudea.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        public IFreeSql Database;
        public ValuesController(IFreeSql freeSql) {
            Database = freeSql;
        }

        [HttpGet]
        public IActionResult Get() {
            return Ok(Database.Select<Student>().FirstAsync().Result.Name);
        }
    }
}
