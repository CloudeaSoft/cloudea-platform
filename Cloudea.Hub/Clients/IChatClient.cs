namespace Cloudea.RealTime.Clients;

public interface IChatClient
{
    Task ReceiveMessage(string message);
}
