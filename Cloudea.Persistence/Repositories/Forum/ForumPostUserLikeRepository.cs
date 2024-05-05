using Cloudea.Domain.Common.Shared;
using Cloudea.Domain.Forum.Entities;
using Cloudea.Persistence;
using Cloudea.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Cloudea.Domain.Forum.Repositories;

public class ForumPostUserLikeRepository : IForumPostUserLikeRepository
{
    private readonly ApplicationDbContext _dbContext;

    public ForumPostUserLikeRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Add(ForumPostUserLike like)
    {
        _dbContext.Set<ForumPostUserLike>().Add(like);
    }

    public async Task<ForumPostUserLike?> GetByUserIdPostIdAsync(
        Guid userId,
        Guid postId,
        CancellationToken cancellationToken = default) =>
        await _dbContext.Set<ForumPostUserLike>()
                        .Where(x => x.OwnerUserId == userId && x.ParentPostId == postId)
                        .FirstOrDefaultAsync(cancellationToken: cancellationToken);


    public void Delete(ForumPostUserLike like)
    {
        _dbContext.Set<ForumPostUserLike>().Remove(like);
    }

    public async Task<List<ForumPostUserLike>> ListByUserIdAsync(
        Guid userId,
        CancellationToken cancellationToken = default) =>
        await _dbContext.Set<ForumPostUserLike>()
                        .Where(x => x.OwnerUserId == userId)
                        .ToListAsync(cancellationToken: cancellationToken);

    public async Task<PageResponse<Guid>> ListPostIdWithPageRequestByUserIdAsync(
    Guid userId,
    PageRequest pageRequest,
    CancellationToken cancellationToken = default) =>
    await _dbContext.Set<ForumPostUserLike>()
        .Where(x => x.OwnerUserId == userId)
        .OrderByDescending(x => x.CreatedOnUtc)
        .Select(x => x.ParentPostId)
        .ToPageListAsync(pageRequest, cancellationToken: cancellationToken);
}
