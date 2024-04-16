using Cloudea.Application.Identity;
using Cloudea.Application.Identity.Contracts;
using Cloudea.Domain.Common.API;
using Cloudea.Domain.Common.Shared;
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

        [HttpGet("Profile/" + ID)]
        [ProducesResponseType(typeof(Result<UserProfile>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Profile(Guid id, CancellationToken c)
        {
            var res = await _service.GetUserProfileAsync(id, c);
            return res.IsSuccess ? Ok(res) : NotFound(res);
        }

        [HttpGet("Profile")]
        [ProducesResponseType(typeof(Result<UserProfile>), StatusCodes.Status200OK)]
        public async Task<IActionResult> MyProfile(CancellationToken c)
        {
            var res = await _service.GetSelfUserProfileAsync(c);
            return res.IsSuccess ? Ok(res) : NotFound(res);
        }

        [HttpPut("Profile")]
        [ProducesResponseType(typeof(Result<UserProfile>), StatusCodes.Status200OK)]
        public async Task<IActionResult> MyProfile([FromBody] UpdateUserProfileRequest request, CancellationToken c)
        {
            var res = await _service.UpdateSelfUserProfileAsync(request, c);
            if (res.IsFailure)
            {
                return HandleFailure(res);
            }
            return Ok(res);
        }

        [HttpPost("Profile/Avatar")]
        [ProducesResponseType(typeof(Result<UserProfile>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Avatar(IFormFile file, CancellationToken c)
        {
            var res = await _service.CreateUserProfileAvatar(file, c);

            if (res.IsFailure)
            {
                return HandleFailure(res);
            }

            return Ok(res);
        }
    }
}
