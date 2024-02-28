using Cloudea;
using Cloudea.Infrastructure.Shared;
using Cloudea.Persistence.Extensions;
using Cloudea.Service.Forum.Domain.Entities;
using Cloudea.Service.Forum.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Cloudea.Persistence.Repositories.Forum;

public class ForumPostRepository(ApplicationDbContext dbContext) : IForumPostRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public void Add(ForumPost newTopic) => _dbContext.Set<ForumPost>().Add(newTopic);

    public async Task<ForumPost?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default) =>
        await _dbContext.Set<ForumPost>().Where(x => x.Id.Equals(id)).FirstOrDefaultAsync(cancellationToken);

    public async Task<PageResponse<ForumPost>> GetWithPageRequestAsync(
        PageRequest request,
        CancellationToken cancellationToken = default) =>
        await _dbContext.Set<ForumPost>().ToPageListAsync(request, cancellationToken);
}
