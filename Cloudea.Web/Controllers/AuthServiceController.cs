using Cloudea.Application.Identity;
using Cloudea.Domain.Common.API;
using Cloudea.Domain.Identity.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cloudea.Web.Controllers
{
    [Authorize]
    public class AuthServiceController : ApiControllerBase
    {
        private readonly AuthService _domainService;

        public AuthServiceController(AuthService domainService)
        {
            _domainService = domainService;
        }

        /// <summary>
        /// 创建Role
        /// </summary>
        /// <param name="name"></param>
        /// <param name="permissions"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Role(string name, List<Permission> permissions, CancellationToken cancellationToken)
        {
            var res = await _domainService.CreateRoleAsync(name, permissions, cancellationToken);
            if (res.IsFailure) {
                return HandleFailure(res);
            }
            return Ok(res);
        }
    }
}
