using Cloudea.Application.Identity;
using Cloudea.Domain.Common.API;
using Cloudea.Domain.Identity.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cloudea.Web.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class UserController : ApiControllerBase
    {
        private readonly UserProfileService _service;

        public UserController(UserProfileService service)
        {
            _service = service;
        }

        [HttpGet("profile/" + ID)]
        public async Task<IActionResult> Profile(Guid id, CancellationToken cancellationToken)
        {
            var res = await _service.GetUserProfileAsync(id, cancellationToken);
            return res.IsSuccess ? Ok(res) : NotFound(res);
        }

        [HttpGet("profile/mine")]
        public async Task<IActionResult> MyProfile(CancellationToken cancellationToken)
        {
            var res = await _service.GetSelfUserProfileAsync(cancellationToken);
            return res.IsSuccess ? Ok(res) : NotFound(res);
        }

        [HttpPost("profile/avatar")]
        [ProducesResponseType(typeof(UserProfile), StatusCodes.Status200OK)]
        public async Task<IActionResult> Avatar(IFormFile file, CancellationToken cancellationToken)
        {
            var res = await _service.CreateUserProfileAvatar(file, cancellationToken);

            if (res.IsFailure)
            {
                return HandleFailure(res);
            }

            return Ok(res);
        }
    }
}
