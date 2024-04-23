using Cloudea.Domain.Common.Shared;
using Cloudea.Domain.Forum.Entities;

namespace Cloudea.Domain.Forum.Repositories
{
    public interface IForumPostRepository
    {
        void Add(ForumPost newTopic);

        void Update(ForumPost newTopic);

        Task<ForumPost?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task<PageResponse<ForumPost>> GetWithPageRequestBySectionIdAsync(
            PageRequest request,
            Guid? sectionId,
            CancellationToken cancellationToken = default);

        Task<PageResponse<ForumPost>> GetWithPageRequestByUserIdTitleContentAsync(
            PageRequest request,
            List<Guid> userIds,
            string title,
            string content,
            CancellationToken cancellationToken = default);
    }
}
