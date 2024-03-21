using Explorer.Blog.API.Enums;
using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Blog.Core.Domain
{
    public class Report : ValueObject
    {
        public int UserId { get; init; }
        public DateTime TimeCommentCreated { get; init; }
        public DateTime TimeReported { get; init; }
        public int ReportAuthorId { get; init; }
        public BlogEnums.ReportReason ReportReason { get; init; }
        public bool IsReviewed { get; private set; } = false;
        public string BlogId { get; init; }
        public string Comment { get; init; }
        public bool? IsAccepted { get; private set; } = null;
        public Report() { }

        [Newtonsoft.Json.JsonConstructor]
        public Report(int userId, DateTime timeCommentCreated, DateTime timeReported, int reportAuthorId, BlogEnums.ReportReason reportReason, bool isReviewed, string blogId, string comment, bool? isAccepted)
        {
            UserId = userId;
            TimeCommentCreated = timeCommentCreated;
            TimeReported = timeReported;
            ReportAuthorId = reportAuthorId;
            ReportReason = reportReason;
            IsReviewed = isReviewed;
            BlogId = blogId;
            Comment = comment;
            IsAccepted = isAccepted;
        }

        public void UpdateReport(Report updatedReport)
        {
            IsReviewed = updatedReport.IsReviewed;
            IsAccepted = updatedReport.IsAccepted;
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return UserId;
            yield return TimeCommentCreated;
            yield return TimeReported;
            yield return ReportAuthorId;
            yield return ReportReason;
            yield return IsReviewed;
            yield return BlogId;
            yield return Comment;
            yield return IsAccepted;
        }
    }
}
