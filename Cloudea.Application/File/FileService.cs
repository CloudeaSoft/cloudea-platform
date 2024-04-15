using Cloudea.Application.Infrastructure;
using Cloudea.Domain.Common.Repositories;
using Cloudea.Domain.Common.Shared;
using Cloudea.Domain.Common.Utils;
using Cloudea.Domain.File.Entities;
using Cloudea.Domain.File.Enums;
using Cloudea.Domain.File.Repositories;
using Microsoft.Extensions.Logging;

namespace Cloudea.Application.File;

public class FileService
{
    private readonly ILogger<FileService> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileRepository _fileRepository;
    private readonly IStorageClient _backupStorage;
    private readonly IStorageClient _remoteStorage;

    public FileService(
        IFileRepository fileRepository,
        IEnumerable<IStorageClient> storageClients,
        IUnitOfWork unitOfWork,
        ILogger<FileService> logger)
    {
        _fileRepository = fileRepository;
        _backupStorage = storageClients.First(c => c.StorageType == StorageType.Public);
        _remoteStorage = storageClients.First(c => c.StorageType == StorageType.Public);
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    /// <summary>
    /// 上传文件
    /// </summary>
    /// <param name="stream"></param>
    /// <param name="fileName"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>文件存储信息</returns>
    public async Task<Result<UploadedFile>> UploadFileAsync(
        Stream stream,
        string fileName,
        CancellationToken cancellationToken = default)
    {
        string hash = HashUtils.ComputeMd5Hash(stream);
        long fileSize = stream.Length;
        DateTimeOffset today = DateTimeOffset.Now;

        // 命名规则
        string key = $"{today.Year}/{today.Month}/{today.Day}/{hash}/{fileName}";

        // 文件查重
        var oldUploadedFile = await _fileRepository.GetBySizeHashAsync(fileSize, hash);
        if (oldUploadedFile is not null)
        {
            return new Error($"Exists: {oldUploadedFile.FileName}. Path: {oldUploadedFile.RemoteUrl}");
        }

        try
        {
            stream.Position = 0;
            // 使用备份存储
            Uri backupUrl = await _backupStorage.SaveFileAsync(key, stream, cancellationToken);
            stream.Position = 0;
            // 使用生产存储
            Uri remoteUrl = await _remoteStorage.SaveFileAsync(key, stream, cancellationToken);
            stream.Position = 0;

            // 返回实体
            Guid id = Guid.NewGuid();
            var newUploadedFile = UploadedFile.Create(id, fileSize, fileName, hash, backupUrl, remoteUrl);

            // 操作数据库插入
            _fileRepository.Add(newUploadedFile);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return newUploadedFile;
        }
        catch (ArgumentException ex)
        {
            _logger.LogCritical(ex.Message);
            return new Error("File.InvalidParam", ex.Message);
        }
    }

    public async Task<Result> DeleteFileAsync(
        Uri path,
        CancellationToken cancellationToken = default)
    {
        var file = await _fileRepository.GetByUriAsync(path, cancellationToken) ?? throw new ArgumentNullException();

        await _remoteStorage.DeleteFileAsync(path, cancellationToken);
        await _backupStorage.DeleteFileAsync(path, cancellationToken);

        _fileRepository.Delete(file);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
