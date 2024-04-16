using Cloudea.Application.File;
using Cloudea.Domain.Common.API;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cloudea.Web.Controllers
{
    [Authorize]
    public class FileController : ApiControllerBase
    {
        private readonly FileService _fileService;

        public FileController(FileService fileService)
        {
            _fileService = fileService;
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="file"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file, CancellationToken cancellationToken)
        {
            string fileName = file.FileName;
            using Stream stream = file.OpenReadStream();
            var res = await _fileService.UploadFileAsync(stream, fileName, cancellationToken: cancellationToken);

            if (res.IsFailure)
            {
                return HandleFailure(res);
            }

            return Ok(res);
        }
    }
}
