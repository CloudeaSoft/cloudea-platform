using Cloudea.Domain.System.Entities;

namespace Cloudea.Domain.System.Repositories
{
    public interface IAnnouncementTranslationRepository
    {
        void Add(AnnouncementTranslation announcement);

        void Update(AnnouncementTranslation announcement);
    }
}
