using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Stakeholders.Core.Domain.Users;
using Explorer.Tours.API.Dtos.Tours;
using Microsoft.Spatial;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Xml.Linq;
using static Explorer.Tours.API.Enums.TourEnums;
using static System.Net.Mime.MediaTypeNames;

namespace Explorer.Tours.Core.Domain.Tours
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Tour : JsonEntity
    {
        [NotMapped][JsonProperty]
        public string Name { get; private set; }
        [NotMapped][JsonProperty]
        public string Description { get; private set; }
        [NotMapped][JsonProperty]
        public int Difficult { get; private set; }
        [NotMapped][JsonProperty]
        public TourStatus Status { get; private set; }
        [NotMapped][JsonProperty]
        public double Price { get; private set; }
        [NotMapped][JsonProperty]
        public List<Point>? Points { get; set; } = new List<Point>();
        [NotMapped][JsonProperty]
        public List<Tag>? Tags { get; private set; } = new List<Tag>();
        [NotMapped][JsonProperty]
        public List<RequiredTime>? RequiredTimes { get; private set; } = new List<RequiredTime>();
        [NotMapped][JsonProperty]
        public List<TourReview>? Reviews { get; set; } = new List<TourReview>();
        /*[NotMapped][JsonProperty]
        public Guide Guide { get; private set; }*/
        [NotMapped][JsonProperty]
        public int AuthorId { get; private set; }
        [NotMapped][JsonProperty]
        public float Length { get; private set; }
        [NotMapped][JsonProperty]
        public DateTime? PublishTime { get; private set; }
        [NotMapped][JsonProperty]
        public DateTime? ArhiveTime { get; private set; }
        [NotMapped]
        [JsonProperty]
        public List<ProblemDto>? Problems { get; set; } = new List<ProblemDto>();
        [NotMapped]
        [JsonProperty]
        public bool? MyOwn { get; private set; }

        public Tour() {}

        [JsonConstructor]
        public Tour(string name, string description, int difficult, TourStatus status, Guide guide, double price, float length, DateTime? publishTime,
            DateTime? arhiveTime, int authorId, List<Point> points, List<Tag> tags, List<RequiredTime> requiredTimes, List<TourReview> reviews, List<ProblemDto> problems, bool myOwn)
        {
            Name = name;
            Description = description;
            Difficult = difficult;
            Status = status;
            Price = price;
            Points = points;
            Tags = tags;
            RequiredTimes = requiredTimes;
            Reviews = reviews;
            //Guide = guide;
            AuthorId = authorId;
            Length = length;
            PublishTime = publishTime;
            ArhiveTime = arhiveTime;
            Problems = problems;
            Validate();
            MyOwn = myOwn;
        }

        private void Validate()
        {
            if (string.IsNullOrEmpty(Name))
                throw new ArgumentException("Invalid name");
            if (string.IsNullOrEmpty(Description))
                throw new ArgumentException("Invalid description");
            if (Difficult <= 0)
                throw new ArgumentException("Invalid diffcult");
            if (Price < 0)
                throw new ArgumentException("Invalid price");
            if (Length < 0)
                throw new ArgumentException("Invalid Length. Length can't be less then 0!");
        }

        public void PublishTour()
        {
            if (Points.Count >= 2 && RequiredTimes.Count != 0 && Tags.Count != 0)
            {
                Status = TourStatus.Published;
                PublishTime = DateTime.Now;
                return;
            }
            throw new ArgumentException("Tour must meet all requirements!");
        }

        public void ArhiveTour()
        {
            if(Status == TourStatus.Published)
            {
                Status = TourStatus.Archived;
                ArhiveTime = DateTime.Now;
                return;
            }
            throw new ArgumentException("Tour needs to be published first!");
        }

        public double GetDistance(double longitude, double latitude, double otherLongitude, double otherLatitude)
        {
            var d1 = latitude * (Math.PI / 180.0);
            var num1 = longitude * (Math.PI / 180.0);
            var d2 = otherLatitude * (Math.PI / 180.0);
            var num2 = otherLongitude * (Math.PI / 180.0) - num1;
            var d3 = Math.Pow(Math.Sin((d2 - d1) / 2.0), 2.0) + Math.Cos(d1) * Math.Cos(d2) * Math.Pow(Math.Sin(num2 / 2.0), 2.0);

            return 6376500.0 * (2.0 * Math.Atan2(Math.Sqrt(d3), Math.Sqrt(1.0 - d3)));
        }

        public bool HasPointsWithinDistance(double longitude, double latitude, int distance)
        {
            if (Status != TourStatus.Published || Points == null) return false;

            foreach (var point in Points)
            {
                var actualDistance = GetDistance(longitude, latitude, point.Longitude, point.Latitude);
                if (point.Public && actualDistance / 1000 <= distance)
                {
                    return true;     
                }
            }
            return false;
        }

        public bool AreEqualPoints(PointDto point1, PointDto point2)
        {

            return point1.Latitude == point2.Latitude && point1.Longitude == point2.Longitude;
        }

        public void PublishPoint(string pointName)
        {
            if (Points == null)
            {
                throw new InvalidOperationException("Points list is not initialized.");
            }

            Point pointToPublish = Points.FirstOrDefault(p => p.Name == pointName);

            if (pointToPublish != null)
            {
                pointToPublish.Public = true;
            }
            else
            {
                throw new ArgumentException("Point with the specified name was not found.");
            }
        }

        public double GetAverageRating()
        {
            if (Reviews == null || Reviews.Count == 0)
            {
                return 0.0;
            }

            double sumOfRatings = 0.0;
            foreach (var review in Reviews)
            {
                sumOfRatings += review.Rating;
            }

            double averageRating = sumOfRatings / Reviews.Count;
            return averageRating;
        }


        public override void ToJson()
        {
            JsonObject = JsonConvert.SerializeObject(this, Formatting.Indented) ??
                         throw new JsonSerializationException("Exception! Could not serialize object!");
        }
        public override void FromJson()
        {
            var tour = JsonConvert.DeserializeObject<Tour>(JsonObject ??
                                                           throw new NullReferenceException(
                                                               "Exception! No object to deserialize!")) ??
                       throw new NullReferenceException("Exception! Tour is null!");
            Name = tour.Name;
            Description = tour.Description;
            Difficult = tour.Difficult;
            Status = tour.Status;
            Price = tour.Price;
            Points = tour.Points;
            Tags = tour.Tags;
            RequiredTimes = tour.RequiredTimes;
            Length = tour.Length;
            Reviews = tour.Reviews;
            //Guide = tour.Guide;
            AuthorId = tour.AuthorId;
            PublishTime = tour.PublishTime;
            ArhiveTime = tour.ArhiveTime;
            Problems = tour.Problems;
            MyOwn= tour.MyOwn;
        }
    }
}