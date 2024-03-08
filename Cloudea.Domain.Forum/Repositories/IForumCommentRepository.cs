using Cloudea.Infrastructure.Shared;
using Cloudea.Service.Forum.Domain.Entities;

namespace Cloudea.Service.Forum.Domain.Repositories
{
    public interface IForumCommentRepository
    {
        void Add(ForumComment comment);

        Task<PageResponse<ForumComment>> GetByReplyIdAndPageRequestAsync(Guid id, PageRequest request, CancellationToken cancellationToken = default);
    }
}
