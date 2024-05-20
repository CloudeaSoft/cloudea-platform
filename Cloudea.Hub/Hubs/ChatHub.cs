using Cloudea.RealTime.Clients;
using Microsoft.AspNetCore.Authorization;
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

    public async Task SendPrivateMessage(string user, string message)
    {
        await Clients.User(user).ReceiveMessage(message);
    }

    public async Task SendGroupMessage(string groupName, string message)
    {
        await Clients.Group(groupName).ReceiveMessage(message);
    }

    public async Task AddToGroup(string groupName)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

        await Clients.Group(groupName)
            .ReceiveMessage($"{Context.ConnectionId} has joined the group {groupName}.");
    }

    public async Task RemoveFromGroup(string groupName)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);

        await Clients.Group(groupName)
            .ReceiveMessage($"{Context.ConnectionId} has left the group {groupName}.");
    }
}
