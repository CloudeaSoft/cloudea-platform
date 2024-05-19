using Cloudea.Domain.System.Repositories;
using Cloudea.RealTime.Clients;
using Microsoft.AspNetCore.SignalR;

namespace Cloudea.RealTime.Hubs
{
    public class SystemHub : Hub<ISystemClient>
    {
        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
        }

        public async Task GetAnnouncement(IAnnouncementRepository _announcementRepository)
        {
            var announcement = await _announcementRepository.GetByIdAsync(Guid.Parse("391909e7-4e01-4e1d-8093-4a1b6e1cbe93"));
            if (announcement is null)
            {
                return;
            }
            await Clients.All.PublishAnnouncement(announcement);
        }
    }
}
