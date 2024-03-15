using Cloudea.Infrastructure.Shared;
using Cloudea.Service.Forum.Domain.Entities;

namespace Cloudea.Service.Forum.Domain.Repositories
{
    public interface IForumCommentRepository
    {
        void Add(ForumComment comment);

        Task<List<ForumComment>> ListByReplyIdsAsync(List<Guid> replyIds,CancellationToken cancellationToken = default);

        Task<PageResponse<ForumComment>> GetByReplyIdAndPageRequestAsync(Guid id, PageRequest request, CancellationToken cancellationToken = default);
    }
}
