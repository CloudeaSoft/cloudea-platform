using Cloudea.Domain.Common.Shared;
using Cloudea.Domain.Forum.Entities;
using Cloudea.Domain.Forum.Repositories;
using Cloudea.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Cloudea.Persistence.Repositories.Forum;

public class ForumSectionRepository(ApplicationDbContext dbContext) : IForumSectionRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task<ForumSection?> GetByIdAsync(
        Guid sectionId,
        CancellationToken cancellationToken = default) =>
        await _dbContext.Set<ForumSection>().Where(x => x.Id == sectionId).FirstOrDefaultAsync(cancellationToken);

    public async Task<List<ForumSection>> GetByIdListAsync(
        List<Guid> idList,
        CancellationToken cancellationToken = default) =>
        await _dbContext.Set<ForumSection>().Where(x => idList.Contains(x.Id)).ToListAsync(cancellationToken);

    public async Task<PageResponse<ForumSection>> GetWithPageRequestAsync(
        PageRequest request,
        CancellationToken cancellationToken = default) =>
        await _dbContext.Set<ForumSection>().OrderBy(x => x.CreatedOnUtc).ToPageListAsync(request, cancellationToken);

    public void Add(ForumSection newSection) => _dbContext.Set<ForumSection>().Add(newSection);

    public void Update(ForumSection newSection) => _dbContext.Set<ForumSection>().Update(newSection);
}
