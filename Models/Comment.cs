namespace LiveCommentsDemo.Models
{
    public class Comment
    {
        public int Id { get; set; }          // Add primary key
        public string User { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public string LiveVideoId { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
