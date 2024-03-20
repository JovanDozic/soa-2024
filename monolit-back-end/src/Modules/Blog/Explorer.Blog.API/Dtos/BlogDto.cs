using static Explorer.Blog.API.Enums.BlogEnums;
namespace Explorer.Blog.API.Dtos
{
    public class BlogDto
    {
        public string Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public BlogStatus Status { get; set; } = BlogStatus.Draft;
        public string[] Images { get; set; }
        public int NetVotes { get; set; }
        public List<BlogRatingDto>? Ratings { get; set; }
        public List<BlogCommentDto>? BlogComments { get; set; }
        public List<ReportDto>? Reports { get; set; }
    }
}
