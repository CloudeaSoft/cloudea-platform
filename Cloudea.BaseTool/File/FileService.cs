using Cloudea.Infrastructure.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Cloudea.Entity.Base.File;

namespace Cloudea.Service.Base.File
{
    public class FileService
    {

        private readonly IFreeSql Database;
        private readonly ILogger<FileService> _logger;

        public FileService(IFreeSql database, ILogger<FileService> logger)
        {
            Database = database;
            _logger = logger;
        }

        public async Task<List<Base_File>> GetFileList(string userName = "Test")
        {
            var list = await Database.Select<Base_File>().ToListAsync();
            return list;
        }

        public async Task<Result> Upload(IFormFile file, string savePath, bool createPathIfNotExist = false)
        {
            try {
                // 判空
                if(file == null) {
                    return Result.Fail("获取文件失败");
                }

                // 判断文件大小
                var fileSize = file.Length;
                if (fileSize > 1024 * 1024 * 10) //10MB
                {
                    return Result.Fail("上传的文件不能大于10M");
                }
                if (fileSize == 0) {
                    return Result.Fail("文件为空");
                }

                // 创建文件夹
                if (!Directory.Exists(savePath) && createPathIfNotExist) {
                    Directory.CreateDirectory(savePath);
                }

                // 获取文件后缀
                var fileExtension = Path.GetExtension(file.FileName);//获取文件格式，拓展名

                // 保存的文件名称(以名称和保存时间命名)
                var saveName = file.FileName.Substring(0, file.FileName.LastIndexOf('.')) + "_" + "" + fileExtension;

                // 文件保存
                using (var fs = System.IO.File.Create(savePath + "" + saveName)) {
                    file.CopyTo(fs);
                    fs.Flush();
                }

                // 完整的文件路径
                var completeFilePath = Path.Combine("", saveName);

                return Result.Success(completeFilePath);
            }
            catch (Exception ex) {
                return Result.Fail("文件保存失败，异常信息为：" + ex.Message);
            }
        }

        public async Task<Result> UploadUser(string fileName)
        {
            try {
                Base_File tempFile = new() {
                    FileName = fileName
                };
                var a = await Database.Insert(tempFile).ExecuteAffrowsAsync();
                Console.WriteLine(a + "在这里");
            }
            catch (Exception ex) {
                return Result.Fail(ex.Message);
            }
            return Result.Success("成功");
        }

        public void Download()
        {

        }
    }
}
