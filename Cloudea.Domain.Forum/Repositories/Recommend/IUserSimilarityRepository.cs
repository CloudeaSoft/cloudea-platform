using Cloudea.Domain.Forum.Entities.Recommend;

namespace Cloudea.Domain.Forum.Repositories.Recommend;

public interface IUserSimilarityRepository
{
    void Add(UserSimilarity userSimilarity);

    void AddOrUpdateRange(ICollection<UserSimilarity> userSimilarities);

    Task<List<UserSimilarity>> ListByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);

    Task<List<UserSimilarity>> ListByUserIdWithLimitAsync(Guid userId, int limit, CancellationToken cancellationToken = default);
}
