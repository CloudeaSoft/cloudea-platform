using Cloudea.Domain.System.Entities;

namespace Cloudea.Domain.System.Repositories
{
    public interface IAnnouncementRepository
    {
        void Add(Announcement announcement);

        void Update(Announcement announcement);

        Task<Announcement?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
