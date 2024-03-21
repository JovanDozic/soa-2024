using Explorer.Blog.API.Dtos;
using Explorer.Blog.Core.Domain;

namespace Explorer.Blog.Core.Converters
{
    public static class BlogCommentConverter
    {
        public static BlogCommentDto ToDto(this BlogComment blogComment)
        {
            if (blogComment == null)
            {
                return null;
            }
            return new BlogCommentDto
            {
                UserId = (int)blogComment.UserId,
                BlogId = blogComment.BlogId,
                Comment = blogComment.Comment,
                TimeCreated = blogComment.TimeCreated,
                TimeUpdated = blogComment.TimeUpdated
            };
        }
        public static BlogComment ToDomain(this BlogCommentDto blogCommentDto)
        {
            return blogCommentDto == null ? null :
                new BlogComment(blogCommentDto.UserId, blogCommentDto.BlogId, blogCommentDto.Comment, blogCommentDto.TimeCreated, blogCommentDto.TimeUpdated);
        }
    }
}
