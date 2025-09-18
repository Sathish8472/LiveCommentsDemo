using LiveCommentsDemo.Data;
using LiveCommentsDemo.Models;
using LiveCommentsDemo.Services;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace LiveCommentsDemo.Hubs
{
    public class CommentsHub : Hub
    {
        private readonly CommentsDbContext _dbContext;
        private readonly DispatcherService _dispatcherService;

        public CommentsHub(CommentsDbContext dbContext, DispatcherService dispatcherService)
        {
            _dbContext = dbContext;
            _dispatcherService = dispatcherService;
        }
        public async Task JoinRoom(string liveVideoId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, liveVideoId);

            var history = await _dbContext.Comments
                .Where(c => c.LiveVideoId == liveVideoId)
                .OrderBy(c => c.Timestamp)
                .ToListAsync();

            await Clients.Caller.SendAsync("LoadHistory", history);
        }
        public async Task SendComment(string liveVideoId, string user, string message)
        {
            var comment = new Comment
            {
                User = user,
                Message = message,
                Timestamp = DateTime.UtcNow,
                LiveVideoId = liveVideoId
            };

            await _dbContext.Comments.AddAsync(comment);
            await _dbContext.SaveChangesAsync();

            await Clients.Group(liveVideoId).SendAsync("ReceiveComment", comment);
        }
    }
}
