using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Blog.Core.Domain
{
    public enum Vote {PLUS = 1, MINUS}
    public class BlogRating: ValueObject
    {
        public long UserId { get; init; }
        public DateTime VotingDate { get; private set; }
        public Vote Mark { get; private set; }


        [Newtonsoft.Json.JsonConstructor]
        public BlogRating(long userId, DateTime votingDate, Vote mark)
        {
            Validate(userId, votingDate);
            UserId = userId;
            VotingDate = votingDate;
            Mark = mark;
        }
        
        public void UpdateRating(BlogRating newRating)
        {
            Mark = newRating.Mark;
            VotingDate = newRating.VotingDate;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return UserId;
            yield return VotingDate;
            yield return Mark;
        }
        private void Validate(long userId, DateTime votingDate)
        {
            if (userId < 0) throw new ArgumentException("Exception! Invalid value of Id");
            if (votingDate.Equals(DateTime.MinValue)) throw new ArgumentException("Exception! Voting date invalid");
        }
    }
}
