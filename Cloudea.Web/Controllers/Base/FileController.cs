using Cloudea.Infrastructure.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Cloudea.Web.Utils.ApiBase;
using Cloudea.Entity.Base;
using System.Security.Permissions;
using Cloudea.Service.Base.File;

namespace Cloudea.Web.Controllers.Base
{
    /// <summary>
    /// 文件管理器接口集
    /// </summary>
    public class FileController : ApiControllerBase
    {

        private readonly FileService fileManager;
        private readonly IWebHostEnvironment env;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileManager"></param>
        /// <param name="database"></param>
        public FileController(FileService fileManager, IWebHostEnvironment env)
        {
            this.fileManager = fileManager;
            this.env = env;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /*[HttpGet]
        public async Task<List<File>> File()
        {
            return await fileManager.GetFileList();
        }*/

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
/*        [HttpPost]
        public async Task<Result> UploadUser(string fileName)
        {
            return await fileManager.UploadUser(fileName);
        }*/

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="files"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> File(IFormFile files, string? filePath = null)
        {
            var res = await fileManager.Upload(files, filePath, true);
            if(res.IsFailure()) {
                return BadRequest(res.Message);
            }
            return Ok(res);
        }
    }
}
