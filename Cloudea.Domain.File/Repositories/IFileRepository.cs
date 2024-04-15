using Cloudea.Domain.File.Entities;

namespace Cloudea.Domain.File.Repositories
{
    public interface IFileRepository
    {
        public void Add(UploadedFile file);

        public void Delete(UploadedFile file);

        /// <summary>
        /// 查找文件是否已存在 - 判断标准：文件大小 | 文件Hash值
        /// </summary>
        /// <param name="fileSize"></param>
        /// <param name="sha256Hash"></param>
        /// <returns></returns>
        Task<UploadedFile?> GetBySizeHashAsync(long fileSize, string sha256Hash);
    }
}
