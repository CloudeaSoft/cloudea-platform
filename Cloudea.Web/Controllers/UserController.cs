using Cloudea.Application.Identity;
using Cloudea.Domain.Common.API;
using Microsoft.AspNetCore.Mvc;

namespace Cloudea.Web.Controllers
{
    public class UserController : ApiControllerBase
    {
        private readonly UserProfileService _service;

        public UserController(UserProfileService service)
        {
            _service = service;
        }

        [HttpGet(ID)]
        public async Task<IActionResult> Profile(Guid userId)
        {
            var res = await _service.GetUserProfileAsync(userId);
            return res.IsSuccess ? Ok(res) : NotFound(res);
        }
    }
}
