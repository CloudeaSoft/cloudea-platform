using Cloudea.Infrastructure.API;
using Cloudea.Infrastructure.Models;
using Cloudea.Service.Auth.Domain.Applications;
using Cloudea.Service.Auth.Domain.Entities;
using Cloudea.Service.Auth.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Cloudea.Web.Controllers
{
    public class AuthServiceController : ApiControllerBase
    {
        private readonly AuthDomainService _domainService;
        private readonly RoleDbContext _roleDbContext;

        public AuthServiceController(AuthDomainService domainService)
        {
            _domainService = domainService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(string name, List<Permission> permissions)
        {
            var createRes = await _domainService.CreateRole(name, permissions);
            if (createRes.IsFailure()) {
                return BadRequest();
            }
            var res = await _roleDbContext.Create(createRes.Data);
            return Ok(res);
        }
    }
}
