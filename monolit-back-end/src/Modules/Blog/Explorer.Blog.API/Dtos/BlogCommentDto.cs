namespace Explorer.Blog.API.Dtos
{
    public class BlogCommentDto
    {
        public int UserId { get; set; }
        public string BlogId { get; set; }
        public string Comment { get; set; } = string.Empty;
        public DateTime TimeCreated { get; set; }
        public DateTime TimeUpdated { get; set; }
    }
}
