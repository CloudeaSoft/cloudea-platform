using Cloudea.Domain.Common.Shared;
using Cloudea.Domain.Forum.Entities;
using Cloudea.Domain.Forum.Entities.Recommend;

namespace Cloudea.Domain.Forum.Repositories;

public interface IForumPostUserHistoryRepository
{
    void Add(ForumPostUserHistory history);

    Task<List<Guid>> ListPostIdByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);

    Task<List<Guid>> ListUserIdByPostIdAsync(Guid userId, CancellationToken cancellationToken = default);

    Task<PageResponse<Guid>> ListPostIdWithPageRequestByUserIdAsync(Guid userId, PageRequest pageRequest, CancellationToken cancellationToken = default);
}
