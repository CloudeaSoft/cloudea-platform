using Cloudea.Domain.System.Entities;
using Cloudea.Domain.System.Repositories;

namespace Cloudea.Persistence.Repositories.System
{
    internal class AnnouncementTranslationRepository : IAnnouncementTranslationRepository
    {
        private readonly ApplicationDbContext _context;

        public AnnouncementTranslationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(AnnouncementTranslation announcement)
        {
            _context.Add(announcement);
        }

        public void Update(AnnouncementTranslation announcement)
        {
            throw new NotImplementedException();
        }
    }
}
