using Cloudea.Domain.Forum.Entities.Recommend;

namespace Cloudea.Domain.Forum.Repositories.Recommend
{
    public interface IPostSimilarityRepository
    {
        void Add(PostSimilarity postSimilarity);

        void AddOrUpdateRange(ICollection<PostSimilarity> postSimilarities);

        Task<List<PostSimilarity>> ListByPostIdAsync(Guid postId, CancellationToken cancellationToken = default);
    }
}
