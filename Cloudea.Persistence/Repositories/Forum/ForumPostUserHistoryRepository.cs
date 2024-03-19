using Cloudea.Domain.Forum.Entities;
using Cloudea.Domain.Forum.Entities.Recommend;
using Cloudea.Domain.Forum.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Cloudea.Persistence.Repositories.Forum;

public class ForumPostUserHistoryRepository : IForumPostUserHistoryRepository
{
    private readonly ApplicationDbContext _dbContext;

    public ForumPostUserHistoryRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Add(ForumPostUserHistory history)
        => _dbContext.Set<ForumPostUserHistory>().Add(history);

    public async Task<List<Guid>> ListUserIdByPostIdAsync(Guid postId, CancellationToken cancellationToken = default)
        => await _dbContext.Set<ForumPostUserHistory>()
            .Where(x => x.PostId == postId)
            .GroupBy(x => x.UserId)
            .Select(g => g.Key)
            .ToListAsync(cancellationToken: cancellationToken);

    public async Task<List<Guid>> ListPostIdByUserIdAsync(Guid userId,CancellationToken cancellationToken = default)
        => await _dbContext.Set<ForumPostUserHistory>()
            .Where(x => x.UserId == userId)
            .GroupBy(x => x.PostId)
            .Select(g => g.Key)
            .ToListAsync(cancellationToken: cancellationToken);
}
