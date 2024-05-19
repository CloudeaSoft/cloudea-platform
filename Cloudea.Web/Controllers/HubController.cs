using Cloudea.Domain.Common.API;
using Cloudea.RealTime.Clients;
using Cloudea.RealTime.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Cloudea.Web.Controllers
{
    public class HubController : ApiControllerBase
    {
        private readonly IHubContext<ChatHub, IChatClient> _chatHubContext;

        public HubController(IHubContext<ChatHub, IChatClient> chatHubContext)
        {
            _chatHubContext = chatHubContext;
        }

        /// <summary>
        /// Broadcast message
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Broadcast(string message)
        {
            await _chatHubContext.Clients.All.ReceiveMessage(message);

            return NoContent();
        }
    }
}
