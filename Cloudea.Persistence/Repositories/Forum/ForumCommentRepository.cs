using Cloudea.Infrastructure.Shared;
using Cloudea.Persistence.Extensions;
using Cloudea.Service.Forum.Domain.Entities;
using Cloudea.Service.Forum.Domain.Repositories;

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

        public Task<PageResponse<ForumComment>> GetByReplyIdAndPageRequestAsync(Guid id, PageRequest request, CancellationToken cancellationToken = default)
        {
            return _context.Set<ForumComment>().Where(x => x.ParentReplyId == id).ToPageListAsync(request, cancellationToken);
        }
    }
}
