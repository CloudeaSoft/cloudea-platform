using Cloudea.Infrastructure.Shared;
using Cloudea.Service.Forum.Domain.Entities;

namespace Cloudea.Service.Forum.Domain.Repositories
{
    public interface IForumPostRepository
    {
        void Add(ForumPost newTopic);

        Task<ForumPost?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task<PageResponse<ForumPost>> GetWithPageRequestAsync(PageRequest request, CancellationToken cancellationToken = default);
    }
}
