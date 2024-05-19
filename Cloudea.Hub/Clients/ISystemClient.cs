using Cloudea.Domain.System.Entities;

namespace Cloudea.RealTime.Clients;

public interface ISystemClient
{
    Task PublishAnnouncement(Announcement announcement);
}
