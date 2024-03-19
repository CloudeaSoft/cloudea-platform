using Cloudea.Domain.Forum.Entities;
using Cloudea.Domain.Forum.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace Cloudea.Persistence.Repositories.Forum;

public class ForumPostUserFavoriteRepository : IForumPostUserFavoriteRepository
{
    private readonly ApplicationDbContext _dbContext;

    public ForumPostUserFavoriteRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Add(ForumPostUserFavorite favorite)
    {
        _dbContext.Set<ForumPostUserFavorite>().Add(favorite);
    }

    public void Delete(ForumPostUserFavorite favorite)
    {
        _dbContext.Set<ForumPostUserFavorite>().Add(favorite);
    }

    public async Task<ForumPostUserFavorite?> GetByUserIdPostIdAsync(
        Guid userId,
        Guid postId,
        CancellationToken cancellationToken = default) =>
        await _dbContext.Set<ForumPostUserFavorite>()
                .Where(x => x.OwnerUserId == userId && x.ParentPostId == postId)
                .FirstOrDefaultAsync(cancellationToken);


    public async Task<List<Guid>> ListPostIdByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
        => await _dbContext.Set<ForumPostUserFavorite>()
                .Where(x => x.OwnerUserId == userId)
                .Select(x => x.ParentPostId)
                .ToListAsync(cancellationToken: cancellationToken);
}
