
using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Explorer.Blog.Core.Converters;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Public;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using static Explorer.Blog.API.Enums.BlogEnums;

namespace Explorer.API.Controllers.Community
{
    [Route("api/blog")]
    public class BlogController : BaseApiController
    {
        private readonly IBlogService _blogService;
        private readonly IUserService _userService;
        private readonly string _msBlogUrl = "http://localhost:8080/ms-blogs";
        static readonly HttpClient _client = new();

        public BlogController(IBlogService blogService, IUserService userService)
        {
            _blogService = blogService;
            _userService = userService;
        }


        [Authorize(Policy = "authorOrTouristPolicy")]
        [HttpPost]
        public async Task<ActionResult<BlogDto>> Create([FromBody] BlogDto blog)
        {
            string uri = $"{_msBlogUrl}/blogs";
            var json = JsonConvert.SerializeObject(blog);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            using HttpResponseMessage response = await _client.PostAsync(uri, data);
            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode);
            }

            string content = await response.Content.ReadAsStringAsync();
            var blogPost = JsonConvert.DeserializeObject<BlogDto>(content);
            return Ok(blogPost);

        }
        /*
        [Authorize(Policy = "authorOrTouristPolicy")]
        [HttpPut("deleteBlog/{blogId}")] // * Updated for Go implementation
        public async Task<ActionResult<BlogDto>> DeleteBlog([FromRoute] string blogId, [FromBody] BlogDto blog)
        {
            string uri = $"{_msBlogUrl}/blogs/delete/{blogId}";
            

            var json = JsonConvert.SerializeObject(blog);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            using HttpResponseMessage response = await _client.PutAsync(uri, data);
            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode);
            }

            string content = response.Content.ReadAsStringAsync().Result;
            var blogComment = JsonConvert.DeserializeObject<BlogCommentDto>(content);
            return Ok(blogComment);
        }
        */

        [AllowAnonymous]
        [HttpGet("get/{blogId}")] // * Updated for Go implementation
        public async Task<ActionResult<PagedResult<BlogDto>>> GetAsync([FromRoute] string blogId)
        {
            string uri = $"{_msBlogUrl}/blogs/{blogId}";
            using HttpResponseMessage response = await _client.GetAsync(uri);
            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode);
            }

            string content = await response.Content.ReadAsStringAsync();

            return CreateResponse(content.ToResult());
        }

        [AllowAnonymous]
        [HttpGet("getAll")] // * Updated for Go implementation
        public async Task<ActionResult<BlogDto>> GetAllAsync([FromQuery] int page, [FromQuery] int pageSize)
        {
            string uri = $"{_msBlogUrl}/blogs/all";
            using HttpResponseMessage response = await _client.GetAsync(uri);
            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode);
            }

            string content = await response.Content.ReadAsStringAsync();
            var blogs = JsonConvert.DeserializeObject<List<BlogDto>>(content);
            var pagedResult = new PagedResult<BlogDto>(blogs, blogs.Count);

            return Ok(pagedResult);
        }
        /*
        [Authorize(Policy = "administratorPolicy")]
        [HttpGet("getReviewedReports")]
        public async Task<ActionResult<ReportDto>> GetReviewedReportsAsync()
        {
            string uri = $"{_msBlogUrl}/comments/reports/reviewed";
            using HttpResponseMessage response = await _client.GetAsync(uri);
            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode);
            }

            string content = await response.Content.ReadAsStringAsync();
            var blogs = JsonConvert.DeserializeObject<List<BlogDto>>(content);
            var pagedResult = new PagedResult<BlogDto>(blogs, blogs.Count);

            return Ok(pagedResult);
        }*/
        
        [Authorize(Policy = "administratorPolicy")]
        [HttpGet("getUnreviewedReports")]
        public async Task<ActionResult<ReportDto>> GetUnreviewedReportsAsync()
        {
            string uri = $"{_msBlogUrl}/comments/reports/unreviewed";
            using HttpResponseMessage response = await _client.GetAsync(uri);
            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode);
            }

            string content = await response.Content.ReadAsStringAsync();
            var reports = JsonConvert.DeserializeObject<List<ReportDto>>(content);
            var pagedResult = new PagedResult<ReportDto>(reports, reports.Count);

            return Ok(pagedResult);
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
        [HttpPost("rate/{blogId}")]
        public async Task<ActionResult<BlogRatingDto>> RateAsync([FromRoute] string blogId, [FromBody] BlogRatingDto rating)
        {
            string uri = $"{_msBlogUrl}/ratings/add/{blogId}";

            var json = JsonConvert.SerializeObject(rating);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            using HttpResponseMessage response = await _client.PostAsync(uri, data);
            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode);
            }
            string content = await response.Content.ReadAsStringAsync();
            var blogRating = JsonConvert.DeserializeObject<BlogRatingDto>(content);
            return Ok(blogRating);

        }

        [Authorize(Policy = "authorOrTouristPolicy")]
        [HttpPatch("publish/{blogId:int}")]
        public ActionResult<BlogRatingDto> Publish([FromRoute] int blogId)
        {
            return CreateResponse(_blogService.PublishBlog(blogId));
        }

        [Authorize(Policy = "authorOrTouristPolicy")]
        [HttpPost("commentBlog/{blogId}")] // * Updated for Go implementation
        public async Task<ActionResult<BlogCommentDto>> CommentBlogAsync([FromRoute] string blogId, [FromBody] BlogCommentDto comment)
        {
            string uri = $"{_msBlogUrl}/comments/add/{blogId}";
            comment.BlogId = blogId; // Just in case

            var json = JsonConvert.SerializeObject(comment);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            using HttpResponseMessage response = await _client.PostAsync(uri, data);
            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode);
            }

            string content = await response.Content.ReadAsStringAsync();
            var blogComment = JsonConvert.DeserializeObject<BlogCommentDto>(content);
            return Ok(blogComment);
        }

        [Authorize(Policy = "authorOrTouristPolicy")]
        [HttpPost("reportBlogComment/{blogId}")] // * Updated for Go implementation
        public async Task<ActionResult<ReportDto>> ReportBlogCommentAsync([FromRoute] string blogId, [FromBody] ReportDto report)
        {
            string uri = $"{_msBlogUrl}/comments/reports";
            report.BlogId = blogId;

            var json = JsonConvert.SerializeObject(report);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            using HttpResponseMessage response = await _client.PostAsync(uri, data);
            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode);
            }

            string content = response.Content.ReadAsStringAsync().Result;
            var blogComment = JsonConvert.DeserializeObject<ReportDto>(content);
            return Ok(blogComment);
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
        [HttpPut("deleteBlogComment/{blogId}")] // * Updated for Go implementation
        public async Task<ActionResult<BlogCommentDto>> DeleteBlogCommentAsync([FromRoute] string blogId, [FromBody] BlogCommentDto comment)
        {
            string uri = $"{_msBlogUrl}/comments/delete/{blogId}";
            comment.BlogId = blogId;

            var json = JsonConvert.SerializeObject(comment);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            using HttpResponseMessage response = await _client.PutAsync(uri, data);
            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode);
            }

            string content = response.Content.ReadAsStringAsync().Result;
            var blogComment = JsonConvert.DeserializeObject<BlogCommentDto>(content);
            return Ok(blogComment);
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
        [HttpPut("didUserReportComment/{blogId}/{userId:int}")] // * Updated for Go implementation
        // 'https://localhost:44333/api/blog/didUserReportComment/aec7e123-233d-4a09-a289-75308ea5b7e6/-23/2024-03-20T12:40:30+01:00'

        public async Task<ActionResult<bool>> DidUserReportCommentAsync([FromRoute] string blogId, [FromRoute] int userId, [FromBody] BlogCommentDto comment)
        {
            string uri = $"{_msBlogUrl}/comments/reports/didUserReport/{userId}/{blogId}";
            string json = JsonConvert.SerializeObject(comment);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            using HttpResponseMessage response = await _client.PutAsync(uri, data);
            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode);
            }

            string content = response.Content.ReadAsStringAsync().Result;
            return Ok(bool.Parse(content));
        }


        [AllowAnonymous]
        [HttpGet("ms-testing")]
        public ActionResult<string> MsTesting()
        {
            return Ok("MS Testing");
        }
    }
}
