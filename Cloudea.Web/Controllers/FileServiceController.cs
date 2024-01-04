using Cloudea.Infrastructure.API;
using Cloudea.Service.File.Domain.Applications;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Mvc;

namespace Cloudea.Web.Controllers
{
    public class FileServiceController : ApiControllerBase
    {
        private readonly FSDomainService _domainService;

        public FileServiceController(FSDomainService domainService)
        {
            _domainService = domainService;
        }

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file, CancellationToken cancellationToken)
        {
            string fileName = file.FileName;
            using Stream stream = file.OpenReadStream();
            var res = await _domainService.UploadFileAsync(stream, fileName, cancellationToken);
            return Ok(res);
        }
    }

    public class UploadRequest
    {
        //不要声明为Action的参数，否则不会正常工作
        public IFormFile File { get; set; }
    }
}
