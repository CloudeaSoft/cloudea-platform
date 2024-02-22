using Cloudea.Infrastructure.API;
using Cloudea.Service.Auth.Domain.Applications;
using Cloudea.Service.Auth.Domain.Entities;
using Cloudea.Service.Auth.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Cloudea.Web.Controllers {
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
    public class AuthServiceController : ApiControllerBase
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
    {
        private readonly AuthDomainService _domainService;
#pragma warning disable CS0649 // 从未对字段“AuthServiceController._roleDbContext”赋值，字段将一直保持其默认值 null
        private readonly RoleDbContext _roleDbContext;
#pragma warning restore CS0649 // 从未对字段“AuthServiceController._roleDbContext”赋值，字段将一直保持其默认值 null

#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
#pragma warning disable CS8618 // 在退出构造函数时，不可为 null 的字段必须包含非 null 值。请考虑声明为可以为 null。
        public AuthServiceController(AuthDomainService domainService)
#pragma warning restore CS8618 // 在退出构造函数时，不可为 null 的字段必须包含非 null 值。请考虑声明为可以为 null。
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
        {
            _domainService = domainService;
        }

        [HttpPost]
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
        public async Task<IActionResult> Create(string name, List<Permission> permissions)
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
        {
            var createRes = await _domainService.CreateRole(name, permissions);
            if (createRes.IsFailure) {
                return BadRequest();
            }
            var res = await _roleDbContext.Create(createRes.Data);
            return Ok(res);
        }
    }
}
