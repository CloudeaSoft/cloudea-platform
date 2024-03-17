using Cloudea.Domain.Common.Shared;
using Cloudea.Domain.Forum.Entities;

namespace Cloudea.Domain.Forum.Repositories
{
    public interface IForumPostRepository
    {
        void Add(ForumPost newTopic);

        void Update(ForumPost newTopic);

        Task<ForumPost?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task<PageResponse<ForumPost>> GetWithPageRequestAsync(PageRequest request, CancellationToken cancellationToken = default);
    }
}
