using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cloudea.Web.Controllers.Base
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ApiControllerBase : ControllerBase
    {
    }
}
