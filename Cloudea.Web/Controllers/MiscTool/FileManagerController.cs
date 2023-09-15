using Cloudea.Entity.MiscTool;
using Cloudea.Infrastructure.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Cloudea.MiscTool;
using Cloudea.Web.Utils.ApiBase;

namespace Cloudea.Web.Controllers.MiscTool
{
    /// <summary>
    /// 文件管理器接口集
    /// </summary>
    public class FileManagerController : ApiControllerBase {

        private readonly FileManager fileManager;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileManager"></param>
        /// <param name="database"></param>
        public FileManagerController(FileManager fileManager, IFreeSql database) {
            this.fileManager = fileManager;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<FileManager_Files>> GetFileList() {
            return await fileManager.GetFileList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Result> Upload(string fileName) {
            return await fileManager.Upload(fileName);
        }
    }
}
