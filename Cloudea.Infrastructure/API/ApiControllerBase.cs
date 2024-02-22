using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cloudea.Infrastructure.API {
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ApiControllerBase : ControllerBase {
        protected const string ID = "{id}";
        protected const string PageRequest = "";//"{page}/{limit}";
    }
}
