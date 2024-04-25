using Cloudea.Domain.Forum.Entities.Recommend;

namespace Cloudea.Domain.Forum.Repositories.Recommend
{
    public interface IUserPostInterestRepository
    {
        void Add(UserPostInterest userPostInterest);

        void AddOrUpdateRange(ICollection<UserPostInterest> userPostInterests);

        Task<List<UserPostInterest>> ListByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);

        Task<List<UserPostInterest>> ListByPostIdAsync(Guid postId, CancellationToken cancellationToken = default);
    }
}
