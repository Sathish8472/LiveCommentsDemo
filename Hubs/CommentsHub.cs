using LiveCommentsDemo.Models;
using Microsoft.AspNetCore.SignalR;

namespace LiveCommentsDemo.Hubs
{
    public class CommentsHub : Hub
    {
        public async Task SendComment(string user, string message)
        {
            var comment = new Comment
            {
                User = user,
                Message = message,
                Timestamp = DateTime.UtcNow
            };

            await Clients.All.SendAsync("ReceiveComment", comment);
        }
    }
}
