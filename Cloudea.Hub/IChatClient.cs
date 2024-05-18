namespace Cloudea.RealTime;

public interface IChatClient
{
    Task ReceiveMessage(string message);
}
