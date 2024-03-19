using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Explorer.Stakeholders.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Explorer.Blog.API.Enums.BlogEnums;

namespace Explorer.API.Controllers.Community
{
    [Route("api/blog")]
    public class BlogController : BaseApiController
    {
        private readonly IBlogService _blogService;
        private readonly IUserService _userService;
        public BlogController(IBlogService blogService, IUserService userService)
        {
            _blogService = blogService;
            _userService = userService;
        }

        [Authorize(Policy = "authorOrTouristPolicy")]
        [HttpPost]
        public ActionResult<BlogDto> Create([FromBody] BlogDto blog)
        {
            var result = _blogService.Create(blog);
            return CreateResponse(result);
        }

        [AllowAnonymous]
        [HttpGet("get/{blogId:int}")]
        public ActionResult<BlogDto> Get([FromRoute] int blogId)
        {
            return CreateResponse(_blogService.Get(blogId));
        }

        [AllowAnonymous]
        [HttpGet("getAll")] // HERE
        public async Task<ActionResult<BlogDto>> GetAllAsync([FromQuery] int page, [FromQuery] int pageSize)
        {
            using HttpClient client = new();
            var response = await client.GetAsync("http://localhost:8080/ms-blogs/all");
            var content = await response.Content.ReadAsStringAsync();
            return Ok(content);

            // Classic architecture: return CreateResponse(_blogService.GetPaged(page, pageSize));
        }

        [Authorize(Policy = "administratorPolicy")]
        [HttpGet("getReviewedReports")]
        public ActionResult<ReportDto> GetReviewedReports()
        {
            var pagedResults = _blogService.GetPaged(1, int.MaxValue).Value.Results;
            var reviewedReports = new List<ReportDto>();

            foreach (var result in pagedResults)
            {
                var reviewed = result.Reports.FindAll(report => report.IsReviewed);
                reviewedReports.AddRange(reviewed);
            }

            return Ok(reviewedReports);
        }

        [Authorize(Policy = "administratorPolicy")]
        [HttpGet("getUnreviewedReports")]
        public ActionResult<ReportDto> GetUnreviewedReports()
        {
            var pagedResults = _blogService.GetPaged(1, int.MaxValue).Value.Results;
            var reviewedReports = new List<ReportDto>();

            foreach (var result in pagedResults)
            {
                var reviewed = result.Reports.FindAll(report => !report.IsReviewed);
                reviewedReports.AddRange(reviewed);
            }

            return Ok(reviewedReports);
        }

        [AllowAnonymous]
        [HttpGet("getFiltered")]
        public ActionResult<BlogDto> GetFiltered([FromQuery] BlogStatus filter)
        {
            return CreateResponse(_blogService.GetFiltered(filter));
        }

        [Authorize(Policy = "authorOrTouristPolicy")]
        [HttpPut("{blogId:int}")]
        public ActionResult<BlogDto> Update([FromBody] BlogDto blog)
        {
            return CreateResponse(_blogService.Update(blog));
        }

        [Authorize(Policy = "authorOrTouristPolicy")]
        [HttpDelete("{blogId:int}")]
        public ActionResult Delete(int blogId)
        {
            return CreateResponse(_blogService.Delete(blogId));
        }

        [AllowAnonymous]
        [HttpPost("rate/{blogId:int}")]
        public ActionResult<BlogRatingDto> Rate([FromRoute] int blogId, [FromBody] BlogRatingDto rating)
        {
            return CreateResponse(_blogService.RateBlog(blogId, rating));
        }

        [Authorize(Policy = "authorOrTouristPolicy")]
        [HttpPatch("publish/{blogId:int}")]
        public ActionResult<BlogRatingDto> Publish([FromRoute] int blogId)
        {
            return CreateResponse(_blogService.PublishBlog(blogId));
        }

        [Authorize(Policy = "authorOrTouristPolicy")]
        [HttpPost("commentBlog/{blogId:int}")]
        public ActionResult<BlogCommentDto> CommentBlog([FromRoute] int blogId, [FromBody] BlogCommentDto comment)
        {
            return CreateResponse(_blogService.CommentBlog(blogId, comment));
        }

        [Authorize(Policy = "authorOrTouristPolicy")]
        [HttpPost("reportBlogComment/{blogId:int}")]
        public ActionResult<ReportDto> ReportBlogComment([FromRoute] int blogId, [FromBody] ReportDto report)
        {
            return CreateResponse(_blogService.CreateReport(blogId, report));
        }

        [Authorize(Policy = "authorOrTouristPolicy")]
        [HttpPut("updateBlogComment/{blogId:int}")]
        public ActionResult<BlogCommentDto> UpdateBlogComment([FromRoute] int blogId, [FromBody] BlogCommentDto comment)
        {
            return CreateResponse(_blogService.UpdateComment(blogId, comment));
        }

        [Authorize(Policy = "administratorPolicy")]
        [HttpPut("reviewReport/{blogId:int}/{isAccepted}")]
        public ActionResult<ReportDto> ReviewReport([FromRoute] int blogId, [FromRoute] bool isAccepted, [FromBody] ReportDto report)
        {
            report.IsReviewed = true;
            report.IsAccepted = isAccepted;
            return CreateResponse(_blogService.UpdateReport(blogId, report));
        }

        [Authorize(Policy = "authorOrTouristPolicy")]
        [HttpPut("deleteBlogComment/{blogId:int}")]
        public ActionResult<BlogCommentDto> DeleteBlogComment([FromRoute] int blogId, [FromBody] BlogCommentDto comment)
        {
            return CreateResponse(_blogService.DeleteComment(blogId, comment));
        }

        [Authorize(Policy = "administratorPolicy")]
        [HttpPut("deleteReportedBlogComment/{blogId:int}")]
        public ActionResult<BlogCommentDto> DeleteReportedBlogComment([FromRoute] int blogId, [FromBody] ReportDto report)
        {
            var pagedResults = _blogService.GetPaged(1, int.MaxValue).Value.Results;
            var comment = new BlogCommentDto();

            _userService.DisableBlogs(report.UserId);

            foreach (var result in pagedResults)
            {
                var reviewed = result.BlogComments.Find(comment => comment.UserId == report.UserId && comment.TimeCreated == report.TimeCommentCreated);
                comment = reviewed;
            }
            return CreateResponse(_blogService.DeleteComment(blogId, comment));
        }

        [Authorize(Policy = "authorOrTouristPolicy")]
        [HttpGet("didUserReportComment/{blogId:int}/{userId:int}/{commentTimeCreated}")]
        public ActionResult<bool> DidUserReportComment([FromRoute] int blogId, [FromRoute] int userId, [FromRoute] DateTime commentTimeCreated)
        {
            var blogs = _blogService.GetPaged(1, int.MaxValue).Value.Results;

            foreach (var blog in blogs)
            {
                if (blog.Reports == null)
                    return Ok(false);

                if (blog.Reports.Any(report => report.ReportAuthorId == userId && report.TimeCommentCreated == commentTimeCreated))
                    return Ok(true);
            }

            return Ok(false);
        }


        [AllowAnonymous]
        [HttpGet("ms-testing")]
        public ActionResult<string> MsTesting()
        {
            return Ok("MS Testing");
        }
    }
}
