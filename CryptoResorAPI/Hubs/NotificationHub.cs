using Microsoft.AspNetCore.SignalR;

namespace CryrptoResorAPI.Hubs
{
    public class NotificationHub : Hub
    {
        public async Task SendMessageAsync(string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", message);
        }
    }
}
