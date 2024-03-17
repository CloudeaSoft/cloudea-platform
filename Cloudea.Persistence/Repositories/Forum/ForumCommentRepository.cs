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
                .ToPageListAsync(request, cancellationToken);
        }

        public async Task<List<ForumComment>> ListByReplyIdsAsync(List<Guid> replyIds, CancellationToken c)
        {
            var set = _context.Set<ForumComment>().AsNoTracking();
            var sorted = set.OrderByDescending(x => x.CreatedOnUtc);
            return await set.Select(x => x.ParentReplyId)
                      .Distinct()
                      .SelectMany(x => sorted.Where(y => y.ParentReplyId == x).Take(2))
                      .ToListAsync(cancellationToken: c);
        }
    }
}
