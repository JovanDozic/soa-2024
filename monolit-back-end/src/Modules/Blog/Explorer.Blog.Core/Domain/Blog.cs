using Explorer.BuildingBlocks.Core.Domain;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using static Explorer.Blog.API.Enums.BlogEnums;

namespace Explorer.Blog.Core.Domain
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Blog : JsonEntity
    {
        [NotMapped]
        [JsonProperty]
        public string Title { get; set; }
        [NotMapped]
        [JsonProperty]
        public string Description { get; set; }
        [NotMapped]
        [JsonProperty]
        public DateTime CreationDate { get; private set; } = DateTime.Now;
        [NotMapped]
        [JsonProperty]
        public BlogStatus Status { get; private set; } = BlogStatus.Draft;
        [NotMapped]
        [JsonProperty]
        public string[]? Images { get; set; }
        [NotMapped]
        [JsonProperty]
        public long UserId { get; private set; }
        [NotMapped]
        [JsonProperty]
        public List<BlogRating>? Ratings { get; private set; } = new List<BlogRating>();
        [NotMapped]
        [JsonProperty]
        public int NetVotes { get; private set; }
        [NotMapped]
        [JsonProperty]
        public List<BlogComment>? BlogComments { get; private set; } = new List<BlogComment>();
        [NotMapped]
        [JsonProperty]
        public List<Report>? Reports { get; set; } = new List<Report>();

        public Blog() { }

        [JsonConstructor]
        public Blog(string title, string description, DateTime creationDate,
            BlogStatus status, string[] images, long userId, int netVotes, List<BlogRating> ratings, List<BlogComment> blogComments, List<Report> reports)
        {
            Validate(title, description);
            Title = title;
            Description = description;
            CreationDate = creationDate;
            Status = status;
            Images = images;
            UserId = userId;
            NetVotes = netVotes;
            Ratings = ratings;
            BlogComments = blogComments;
            Reports = reports;
        }

        public Blog(string title, string description, DateTime creationDate,
            BlogStatus status, string[] images, long userId)
        {
            Validate(title, description);
            Title = title;
            Description = description;
            CreationDate = creationDate;
            Status = status;
            Images = images;
            UserId = userId;
        }

        public void PublishBlog()
        {
            if (Status != BlogStatus.Draft)
                return;
            Status = BlogStatus.Published;
        }
        public void Rate(BlogRating rating)
        {
            BlogRating oldRating = Ratings.Find(x => x.UserId == rating.UserId);
            UpdateRatings(rating, oldRating);
            CalculateNetVotes();
            UpdateBlogStatus();
        }
        private void UpdateRatings(BlogRating rating, BlogRating? oldRating)
        {
            if (oldRating == null)
            {
                Ratings.Add(rating);
            }
            else if (oldRating.Mark == rating.Mark)
            {
                Ratings.Remove(oldRating);
            }
            else if (oldRating.Mark != rating.Mark)
            {
                oldRating.UpdateRating(rating);
            }
        }

        private void CalculateNetVotes()
        {
            var positiveRatings = Ratings.Count(x => x.Mark == Vote.PLUS);
            var negativeRatings = Ratings.Count(x => x.Mark == Vote.MINUS);
            NetVotes = positiveRatings - negativeRatings;
        }
        private void UpdateBlogStatus()
        {
            switch (NetVotes)
            {
                case -10:
                    Status = BlogStatus.Closed; break;
                case var n when n > 100:
                    Status = BlogStatus.Active; break;
                case var n when n > 500:
                    Status = BlogStatus.Famous; break;
            }
        }
        private static void Validate(string title, string description)
        {
            if (string.IsNullOrEmpty(title)) throw new ArgumentException("Invalid or empty title.");
            if (string.IsNullOrEmpty(description)) throw new ArgumentException("Invalid or empty description.");

        }
        public void UpdateComments(BlogComment comment)
        {
            BlogComment oldComment = BlogComments.Find(x =>
                x.UserId == comment.UserId && x.TimeCreated == comment.TimeCreated);
            oldComment.UpdateComment(comment);
        }

        public void UpdateReports(Report report)
        {
            Report oldReport = Reports.Find(x => x.UserId == report.UserId && x.TimeReported == report.TimeReported);
            oldReport.UpdateReport(report);
        }

        public override void ToJson()
        {
            JsonObject = JsonConvert.SerializeObject(this, Formatting.Indented) ??
                         throw new JsonSerializationException("Exception! Could not serialize object!");
        }
        public override void FromJson()
        {
            var blog = JsonConvert.DeserializeObject<Blog>(JsonObject ??
                                                           throw new NullReferenceException(
                                                               "Exception! No object to deserialize!")) ??
                       throw new NullReferenceException("Exception! Blog is null!");
            Title = blog.Title;
            Description = blog.Description;
            CreationDate = blog.CreationDate;
            Status = blog.Status;
            Images = blog.Images;
            UserId = blog.UserId;
            Ratings = blog.Ratings;
            NetVotes = blog.NetVotes;
            BlogComments = blog.BlogComments;
            Reports = blog.Reports;
        }
    }
}