using Cloudea.Domain.Identity.Entities;

namespace Cloudea.Domain.Identity.Repositories
{
    public interface IUserProfileRepository
    {
        void Add(UserProfile user);

        void Update(UserProfile user);

        void Delete(UserProfile user);

        Task<UserProfile?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);

        Task<List<UserProfile>> ListByUserIdAsync(ICollection<Guid> userIdList, CancellationToken cancellationToken = default);

        Task<List<Guid>> ListUserIdByDisplayNameAsync(string displayName, CancellationToken cancellationToken = default);
    }
}
