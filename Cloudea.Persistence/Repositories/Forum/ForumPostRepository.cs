using Cloudea.Domain.Common.Shared;
using Cloudea.Domain.Forum.Entities;
using Cloudea.Domain.Forum.Repositories;
using Cloudea.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Cloudea.Persistence.Repositories.Forum;

public class ForumPostRepository(ApplicationDbContext dbContext) : IForumPostRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public void Add(ForumPost newTopic) => _dbContext.Set<ForumPost>().Add(newTopic);

    public void Update(ForumPost newTopic) => _dbContext.Set<ForumPost>().Update(newTopic);

    public async Task<ForumPost?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default) =>
        await _dbContext.Set<ForumPost>().Where(x => x.Id.Equals(id)).FirstOrDefaultAsync(cancellationToken);

    public async Task<PageResponse<ForumPost>> GetWithPageRequestSectionIdAsync(
        PageRequest request,
        Guid? sectionId,
        CancellationToken cancellationToken = default) =>
        await _dbContext.Set<ForumPost>()
            .Where(x => sectionId == null || sectionId == x.ParentSectionId)
            .OrderBy(x => x.CreatedOnUtc)
            .ToPageListAsync(request, cancellationToken);
}
