using Cloudea.Service.File.Domain.Entities;

namespace Cloudea.Service.File.Domain.Abstractions
{
    public interface IFSRepository
    {
        /// <summary>
        /// 查找文件是否已存在 - 判断标准：文件大小 | 文件Hash值
        /// </summary>
        /// <param name="fileSize"></param>
        /// <param name="sha256Hash"></param>
        /// <returns></returns>
        Task<File_UploadedFile?> FindFileAsync(long fileSize, string sha256Hash);
    }
}
