using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace HighCinema.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string senderId, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", senderId, message);
        }

    }
}
