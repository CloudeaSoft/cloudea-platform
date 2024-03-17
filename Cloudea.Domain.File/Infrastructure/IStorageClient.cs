using Cloudea.Domain.File.Enums;

namespace Cloudea.Domain.File.Infrastructure
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
    }
}
