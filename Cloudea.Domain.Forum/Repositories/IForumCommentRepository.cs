using Cloudea.Domain.Common.Shared;
using Cloudea.Domain.Forum.Entities;

namespace Cloudea.Domain.Forum.Repositories
{
    public interface IForumCommentRepository
    {
        void Add(ForumComment comment);

        Task<List<ForumComment>> ListByReplyIdsAsync(List<Guid> replyIds, CancellationToken cancellationToken = default);

        Task<PageResponse<ForumComment>> GetByReplyIdAndPageRequestAsync(Guid id, PageRequest request, CancellationToken cancellationToken = default);
    }
}
