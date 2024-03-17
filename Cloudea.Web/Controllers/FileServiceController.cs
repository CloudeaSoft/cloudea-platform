using Cloudea.Application.File;
using Cloudea.Domain.Common.API;
using Microsoft.AspNetCore.Mvc;

namespace Cloudea.Web.Controllers
{
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
    public class FileServiceController : ApiControllerBase
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
    {
        private readonly FileService _domainService;

#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
        public FileServiceController(FileService domainService)
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
        {
            _domainService = domainService;
        }

        [HttpPost]
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
        public async Task<IActionResult> Upload(IFormFile file, CancellationToken cancellationToken)
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
        {
            string fileName = file.FileName;
            using Stream stream = file.OpenReadStream();
            var res = await _domainService.UploadFileAsync(stream, fileName, cancellationToken);
            return Ok(res);
        }
    }

#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
    public class UploadRequest
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
    {
        //不要声明为Action的参数，否则不会正常工作
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
#pragma warning disable CS8618 // 在退出构造函数时，不可为 null 的字段必须包含非 null 值。请考虑声明为可以为 null。
        public IFormFile File { get; set; }
#pragma warning restore CS8618 // 在退出构造函数时，不可为 null 的字段必须包含非 null 值。请考虑声明为可以为 null。
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
    }
}
