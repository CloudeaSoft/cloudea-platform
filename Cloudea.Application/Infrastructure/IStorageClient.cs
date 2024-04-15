using Cloudea.Domain.Common.Shared;
using Cloudea.Domain.File.Enums;

namespace Cloudea.Application.Infrastructure
{
    public interface IStorageClient
    {
        StorageType StorageType { get; }

        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="key"></param>
        /// <param name="content"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Uri> SaveFileAsync(string key, Stream content, CancellationToken cancellationToken = default);

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="key"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> DeleteFileAsync(Uri path, CancellationToken cancellationToken = default);
    }
}
