using Cloudea.Entity.MiscTool;
using Cloudea.Infrastructure.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiscTool;

namespace Cloudea.Web.Controllers.MiscTool {

    public class FileManagerController : ApiControllerBase {

        private readonly FileManager fileManager;

        public FileManagerController(FileManager fileManager, IFreeSql database) {
            this.fileManager = fileManager;
        }

        [HttpGet]
        public async Task<List<FileManager_Files>> GetFileList() {
            return await fileManager.GetFileList();
        }

        [HttpPost]
        public async Task<Result> Upload(string fileName) {
            return await fileManager.Upload(fileName);
        }
    }
}
