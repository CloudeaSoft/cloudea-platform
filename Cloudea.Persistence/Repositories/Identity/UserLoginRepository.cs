using Cloudea.Domain.Identity.Entities;
using Cloudea.Service.Auth.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System.Threading.Channels;

namespace Cloudea.Persistence.Repositories.Identity;

public class UserLoginRepository : IUserLoginRepository
{

    private readonly IMemoryCache _memoryCache;
    private readonly ApplicationDbContext _dbContext;

    private readonly Channel<UserLogin> waitForInsert = null;
    private readonly ILogger<UserLoginRepository> _logger;

    public UserLoginRepository(IMemoryCache memoryCache, ApplicationDbContext dbContext, ILogger<UserLoginRepository> logger)
    {
        _memoryCache = memoryCache;
        _dbContext = dbContext;
        _logger = logger;

        waitForInsert = Channel.CreateUnbounded<UserLogin>();


        Task.Run(async () => {
            while (await waitForInsert.Reader.WaitToReadAsync()) {
                try {
                    var entity = await waitForInsert.Reader.ReadAsync();
                    bool exist = await _dbContext.Set<UserLogin>()
                        .Where(t => t.UserId == entity.UserId)
                        .Where(t => t.Date == entity.Date && t.Hour == entity.Hour)
                        .AnyAsync();

                    if (exist == false) {
                        // 不存在 添加到数据库
                        _dbContext.Add(entity);
                        throw new NotImplementedException();
                    }
                }
                catch (Exception ex) {
                    _logger.LogError(ex, ex.Message);
                }
            }
        });
    }

    // EMPLOYEE_LOGIN_PREFIX{date},{hour}
    const string USER_LOGIN_PREFIX = "user_login:";

    /// <summary>
    /// 记录 登录 (每小时去重)
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public void RecordLogin(Guid userId)
    {
        string time = DateTime.Now.Hour.ToString();
        string date = DateTime.Now.ToString("yyyy-MM-dd");

        string key = $"{USER_LOGIN_PREFIX}{userId}{date}{time}";
        // 已经保存过了则跳过
        if (_memoryCache.Get(key) != null) {
            return;
        }

        // 一个小时过期时间
        var ttl = DateTime.Parse(DateTime.Now.AddHours(1).ToString("yyyy-MM-dd HH:00:00"));

        // 设置一下缓存 保证下次进来的时候不访问数据库  下一个小时过期
        _memoryCache.Set(key, true, ttl);

        _ = waitForInsert.Writer.WriteAsync(new UserLogin() {
            Date = date,
            UserId = userId,
            Hour = time
        });
    }
}