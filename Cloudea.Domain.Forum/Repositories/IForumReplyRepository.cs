using Cloudea.Domain.Common.Shared;
using Cloudea.Domain.Forum.Entities;

namespace Cloudea.Domain.Forum.Repositories
{
    public interface IForumReplyRepository
    {
        Task<ForumReply?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task<PageResponse<ForumReply>> GetByPostIdWithPageRequestAsync(Guid topicId, PageRequest request, CancellationToken cancellationToken = default);

        void Add(ForumReply reply);
    }
}
