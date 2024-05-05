using Cloudea.Domain.Common.Shared;
using Cloudea.Domain.Forum.Entities;

namespace Cloudea.Domain.Forum.Repositories;

public interface IForumPostUserFavoriteRepository
{
    void Add(ForumPostUserFavorite favorite);

    void Delete(ForumPostUserFavorite favorite);

    Task<ForumPostUserFavorite?> GetByUserIdPostIdAsync(Guid userId, Guid postId, CancellationToken cancellationToken = default);

    Task<List<Guid>> ListPostIdByUserIdAsync(Guid userId,CancellationToken cancellationToken = default);

    Task<PageResponse<Guid>> ListPostIdWithPageRequestByUserIdAsync(Guid userId, PageRequest pageRequest, CancellationToken cancellationToken = default);
}
