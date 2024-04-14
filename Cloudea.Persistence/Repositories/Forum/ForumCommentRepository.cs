using Cloudea.Domain.Common.Shared;
using Cloudea.Domain.Forum.Entities;
using Cloudea.Domain.Forum.Repositories;
using Cloudea.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Cloudea.Persistence.Repositories.Forum
{
    public class ForumCommentRepository : IForumCommentRepository
    {
        private readonly ApplicationDbContext _context;

        public ForumCommentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(ForumComment comment)
        {
            _context.Set<ForumComment>().Add(comment);
        }

        public async Task<PageResponse<ForumComment>> GetByReplyIdAndPageRequestAsync(Guid id, PageRequest request, CancellationToken cancellationToken = default)
        {
            return await _context.Set<ForumComment>()
                .Where(x => x.ParentReplyId == id)
                .OrderBy(x => x.CreatedOnUtc)
                .ToPageListAsync(request, cancellationToken);
        }
    }
}
