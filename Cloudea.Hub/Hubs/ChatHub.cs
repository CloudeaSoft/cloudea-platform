using Cloudea.RealTime.Clients;
using Microsoft.AspNetCore.SignalR;

namespace Cloudea.RealTime.Hubs;

public class ChatHub : Hub<IChatClient>
{
    public override async Task OnConnectedAsync()
    {
        await Clients.All.ReceiveMessage(
            $"{Context.ConnectionId} has joined.");
    }

    public async Task SendMessage(string message)
    {
        await Clients.All.ReceiveMessage(
            $"{Context.ConnectionId}/{DateTime.Now}: {message}");
    }
}
