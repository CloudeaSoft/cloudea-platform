using Cloudea.RealTime.Hubs;
using Microsoft.AspNetCore.Builder;

namespace Cloudea.RealTime;

public static class WebApplicationExtensions
{
    public static void MapCloudeaHubs(this WebApplication app)
    {
        app.MapHub<ChatHub>("/chat-hub");
        app.MapHub<SystemHub>("/system-hub");
    }
}
