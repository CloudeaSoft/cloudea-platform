using Cloudea.Application.Infrastructure;
using Cloudea.Domain.File.Enums;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Quartz.Util;

namespace Cloudea.Infrastructure.FileStorageClients;
/// <summary>
/// 把本地服务器当成一个云存储服务器，是一个Mock。文件保存在wwwroot文件夹下。
/// 这仅供开发、演示阶段使用，在生产环境中，一定要用专门的云存储服务器来代替。
/// </summary>
class DefaultStorageClient : IStorageClient
{
    public StorageType StorageType => StorageType.Public;
    private readonly IWebHostEnvironment hostEnv;
    private readonly IHttpContextAccessor httpContextAccessor;

    public DefaultStorageClient(IWebHostEnvironment hostEnv, IHttpContextAccessor httpContextAccessor)
    {
        this.hostEnv = hostEnv;
        this.httpContextAccessor = httpContextAccessor;
    }

    public async Task<Uri> SaveFileAsync(string key, Stream content, CancellationToken cancellationToken = default)
    {
        if (key.StartsWith('/'))
        {
            throw new ArgumentException("key should not start with /", nameof(key));
        }
        string workingDir = Path.Combine(hostEnv.ContentRootPath, "wwwroot", "File");

        string fullPath = Path.Combine(workingDir, key);
        string? fullDir = Path.GetDirectoryName(fullPath);//get the directory
        if (!Directory.Exists(fullDir) && !string.IsNullOrWhiteSpace(fullDir))//automatically create dir
        {
            Directory.CreateDirectory(fullDir);
        }
        if (File.Exists(fullPath))//如果已经存在，则尝试删除
        {
            File.Delete(fullPath);
        }

        using Stream outStream = File.OpenWrite(fullPath);
        await content.CopyToAsync(outStream, cancellationToken);
        HttpRequest req = httpContextAccessor.HttpContext!.Request;
        string url = req.Scheme + "://" + req.Host + "/File/" + key;
        return new Uri(url);
    }

    public async Task<bool> DeleteFileAsync(Uri path, CancellationToken cancellationToken = default)
    {
        // 验证path  
        if (path is null || path.ToString().IsNullOrWhiteSpace())
        {
            throw new ArgumentNullException(nameof(path));
        }

        // 在这里构件真实文件路径
        string fullPath = Path.Combine(hostEnv.ContentRootPath, "wwwroot", path.LocalPath.TrimStart('/'));

        // 检查文件是否存在  
        if (!File.Exists(fullPath))
        {
            // 文件不存在，返回false表示未删除  
            return false;
        }

        // 删除文件  
        await Task.Run(() => File.Delete(fullPath), cancellationToken);

        // 返回true表示物理删除成功  
        return true;
    }
}
