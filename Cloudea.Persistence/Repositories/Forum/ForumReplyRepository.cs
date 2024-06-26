﻿using Cloudea.Domain.Common.Shared;
using Cloudea.Domain.Forum.Entities;
using Cloudea.Domain.Forum.Repositories;
using Cloudea.Persistence.Extensions;
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

    public async Task<PageResponse<ForumReply>> GetByPostIdWithPageRequestAsync(
        Guid topicId,
        PageRequest request,
        CancellationToken cancellationToken = default) =>
        await _dbContext.Set<ForumReply>()
                        .Where(x => x.ParentPostId == topicId)
                        .OrderBy(x => x.CreatedOnUtc)
                        .ToPageListAsync(request, cancellationToken);

    public void Update(ForumReply reply) =>
        _dbContext.Set<ForumReply>().Update(reply);
}
