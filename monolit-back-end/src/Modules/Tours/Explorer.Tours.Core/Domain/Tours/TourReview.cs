using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Tours.Core.Domain.Tours
{
    public class TourReview : ValueObject
    {

        public int Rating { get; set; }

        public string Comment { get; set; }

        public int TouristId { get; set; }

        public string TouristUsername { get; set; }

        public DateTime TourDate { get; set; }

        public DateTime CreationDate { get; set; } = DateTime.Now;

        public List<string> Images { get; set; }

        [Newtonsoft.Json.JsonConstructor]
        public TourReview(int rating, string comment, int touristId, string touristUsername, DateTime tourDate, DateTime creationDate, List<string> images)
        {
            Rating = rating;
            Comment = comment;
            TouristId = touristId;
            TouristUsername = touristUsername;
            TourDate = tourDate;
            CreationDate = creationDate;
            Images = images ?? new List<string>();
            Validate();
        }
        private void Validate()
        {
            if (Rating < 1 || Rating > 5) throw new ArgumentException("Invalid rating");
            if (string.IsNullOrWhiteSpace(Comment)) throw new ArgumentException("Invalid or empty comment");
            if (TouristId == 0) throw new ArgumentException("Invalid TouristId");
            if (string.IsNullOrWhiteSpace(TouristUsername)) throw new ArgumentException("Invalid or empty tourist username");
            //if (TourDate > DateTime.Now) throw new ArgumentException("Invalid tour date.");
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Rating;
            yield return TouristId;
            yield return TouristUsername;
            yield return TourDate;
            yield return CreationDate;
            yield return Images;
            yield return Comment;
        }
    }
}