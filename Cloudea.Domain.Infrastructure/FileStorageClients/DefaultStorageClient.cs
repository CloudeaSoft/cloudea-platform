using Cloudea.Application.Infrastructure;
using Cloudea.Domain.File.Enums;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Quartz.Util;

namespace Cloudea.Infrastructure.FileStorageClients;
/// <summary>
/// 把本地服务器当成一个云存储服务器。文件保存在 wwwroot 文件夹下
/// 仅供开发、演示阶段使用
/// </summary>
class DefaultStorageClient : IStorageClient
{
    public StorageType StorageType => StorageType.Public;
    private readonly IWebHostEnvironment _hostEnv;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly string _httpScheme = "https";

    public DefaultStorageClient(IWebHostEnvironment hostEnv, IHttpContextAccessor httpContextAccessor)
    {
        _hostEnv = hostEnv;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<Uri> SaveFileAsync(string key, Stream content, CancellationToken cancellationToken = default)
    {
        if (key.StartsWith('/'))
        {
            throw new ArgumentException("key should not start with /", nameof(key));
        }
        string workingDir = Path.Combine(_hostEnv.ContentRootPath, "wwwroot", "File");

        string fullPath = Path.Combine(workingDir, key);
        string? fullDir = Path.GetDirectoryName(fullPath); // Get the dir
        if (!Directory.Exists(fullDir) && !string.IsNullOrWhiteSpace(fullDir)) // Automatically create dir
        {
            Directory.CreateDirectory(fullDir);
        }
        if (File.Exists(fullPath)) // Delete if exists
        {
            File.Delete(fullPath);
        }

        using Stream outStream = File.OpenWrite(fullPath);
        await content.CopyToAsync(outStream, cancellationToken);
        HttpRequest req = _httpContextAccessor.HttpContext!.Request;
        string url = _httpScheme + "://" + req.Host + "/File/" + key;
        return new Uri(url);
    }

    public async Task<bool> DeleteFileAsync(Uri path, CancellationToken cancellationToken = default)
    {
        // 检查 path
        if (path is null || path.ToString().IsNullOrWhiteSpace())
        {
            throw new ArgumentNullException(nameof(path));
        }

        // 在这里构件真实文件路径
        string fullPath = Path.Combine(_hostEnv.ContentRootPath, "wwwroot", path.LocalPath.TrimStart('/'));

        // 检查文件是否存在  
        if (!File.Exists(fullPath))
        {
            // 文件不存在，返回 false 表示未删除  
            return false;
        }

        // 删除文件  
        await Task.Run(() => File.Delete(fullPath), cancellationToken);

        // 返回 true 表示物理删除成功  
        return true;
    }
}
