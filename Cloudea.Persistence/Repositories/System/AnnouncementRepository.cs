using Cloudea.Domain.System.Entities;
using Cloudea.Domain.System.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Cloudea.Persistence.Repositories.System
{
    internal class AnnouncementRepository : IAnnouncementRepository
    {
        private readonly ApplicationDbContext _context;

        public AnnouncementRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(Announcement announcement)
        {
            _context.Add(announcement);
        }

        public async Task<Announcement?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken = default) =>
            await _context.Set<Announcement>()
                .Where(x => x.Id == id)
                .Include(x => x.Translations)
                .FirstOrDefaultAsync(cancellationToken);

        public void Update(Announcement announcement)
        {
            throw new NotImplementedException();
        }
    }
}
