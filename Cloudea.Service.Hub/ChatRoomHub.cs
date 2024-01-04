using Microsoft.AspNetCore.SignalR;

namespace Cloudea.Service.HubTest
{
    public class ChatRoomHub : Hub
    {
        public Task SendMessage(string message)
        {
            string connId = this.Context.ConnectionId;
            string msgToSend = $"{connId}/{DateTime.Now}: {message}";
            return Clients.All.SendAsync("PublicMsgReceived", msgToSend);
        }
    }
}
