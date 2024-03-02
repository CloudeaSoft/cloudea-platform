using Cloudea.Infrastructure.Utils;
using Cloudea.Service.File.Domain.Abstractions;
using Cloudea.Service.File.Domain.Entities;
using Cloudea.Service.File.Domain.Enums;
using Cloudea.Service.File.Infrastructure;

namespace Cloudea.Service.File.Domain.Applications;

public class FileService
{
    private readonly IFSRepository _fsRepository;
    private readonly IStorageClient _backupStorage;
    private readonly IStorageClient _remoteStorage;

    public FileService(
        IFSRepository fsRepository,
        IEnumerable<IStorageClient> storageClients)
    {
        _fsRepository = fsRepository;
        _backupStorage = storageClients.First(c => c.StorageType == StorageType.Public);
        _remoteStorage = storageClients.First(c => c.StorageType == StorageType.Public);
    }

    /// <summary>
    /// 上传文件
    /// </summary>
    /// <param name="stream"></param>
    /// <param name="fileName"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>文件存储信息</returns>
    public async Task<File_UploadedFile> UploadFileAsync(
        Stream stream,
        string fileName,
        CancellationToken cancellationToken)
    {
        string hash = HashUtils.ComputeMd5Hash(stream);
        long fileSize = stream.Length;
        DateTime today = DateTime.Now;
        // 命名规则
        string key = $"{today.Year}/{today.Month}/{today.Day}/{hash}/{fileName}";

        // 文件查重
        var oldUploadItem = await _fsRepository.FindFileAsync(fileSize, hash);
        if (oldUploadItem != null)
        {
            return oldUploadItem;
        }
        stream.Position = 0;
        // 使用备份存储
        Uri backupUrl = await _backupStorage.SaveFileAsync(key, stream, cancellationToken);
        stream.Position = 0;
        // 使用生产存储
        Uri remoteUrl = await _remoteStorage.SaveFileAsync(key, stream, cancellationToken);
        stream.Position = 0;
        Guid id = Guid.NewGuid();
        // 返回实体，由应用服务操作数据库插入
        return File_UploadedFile.Create(id, fileSize, fileName, hash, backupUrl, remoteUrl);
    }
}
