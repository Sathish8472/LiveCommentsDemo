using LiveCommentsDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace LiveCommentsDemo.Data
{
    public class CommentsDbContext : DbContext
    {
        public CommentsDbContext(DbContextOptions<CommentsDbContext> options) : base(options)
        {

        }

        public DbSet<Comment> Comments { get; set; } = null!;
    }
}
