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
        public async Task<IActionResult> Profile(Guid id)
        {
            var res = await _service.GetUserProfileAsync(id);
            return res.IsSuccess ? Ok(res) : NotFound(res);
        }

        [HttpGet]
        public async Task<IActionResult> MyProfile()
        {
            var res = await _service.GetSelfUserProfileAsync();
            return res.IsSuccess ? Ok(res) : NotFound(res);
        }
    }
}
