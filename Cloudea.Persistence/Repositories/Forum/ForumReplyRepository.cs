using Cloudea;
using Cloudea.Infrastructure.Shared;
using Cloudea.Persistence.Extensions;
using Cloudea.Service.Forum.Domain.Entities;
using Cloudea.Service.Forum.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Cloudea.Persistence.Repositories.Forum;

public class ForumReplyRepository(ApplicationDbContext context) : IForumReplyRepository
{
    private readonly ApplicationDbContext _dbContext = context;

    public void Add(ForumReply reply) => _dbContext.Set<ForumReply>().Add(reply);

    public async Task<ForumReply?> GetByIdAsync(
        Guid replyId,
        CancellationToken cancellationToken = default) =>
        await _dbContext.Set<ForumReply>()
                        .Where(x => x.Id == replyId)
                        .FirstOrDefaultAsync(cancellationToken);

    public async Task<PageResponse<ForumReply>> GetByTopicIdWithPageRequestAsync(
        Guid topicId,
        PageRequest request,
        CancellationToken cancellationToken = default) =>
        await _dbContext.Set<ForumReply>()
                        .Where(x => x.ParentPostId == topicId)
                        .ToPageListAsync(request, cancellationToken);
}
