using Explorer.Blog.API.Dtos;
using Explorer.BuildingBlocks.Core.UseCases;
using FluentResults;
using static Explorer.Blog.API.Enums.BlogEnums;

namespace Explorer.Blog.API.Public
{
    public interface IBlogService
    {
        Result<BlogDto> Get(int id);
        Result<BlogDto> Create(BlogDto blog);
        Result<PagedResult<BlogDto>> GetPaged(int page, int pageSize);
        Result<List<BlogDto>> GetFiltered(BlogStatus filter);
        Result<BlogDto> Update(BlogDto blog);
        Result Delete(int blogId);
        Result<BlogDto>RateBlog(int blogId, BlogRatingDto rating);
        Result<BlogDto> PublishBlog(int blogId);
        Result<BlogDto> CommentBlog(int blogId, BlogCommentDto comment);
        Result<BlogDto> UpdateComment(int blogId, BlogCommentDto comment);
        Result<BlogDto> DeleteComment(int blogId, BlogCommentDto comment);
        Result<BlogDto> UpdateReport(int blogId, ReportDto report);
        Result<BlogDto> CreateReport(int blogId, ReportDto report);
    }
}
