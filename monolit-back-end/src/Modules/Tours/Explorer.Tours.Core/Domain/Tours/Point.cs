using Explorer.BuildingBlocks.Core.Domain;
namespace Explorer.Tours.Core.Domain.Tours
{
    public class Point : ValueObject
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Picture { get; set; }
        public bool Public { get; set; }
        public Point() {}

        [Newtonsoft.Json.JsonConstructor]
        public Point(double latitude, double longitude, string name, string description, string picture, bool isPublic = false)
        {
            Latitude = latitude;
            Longitude = longitude;
            Name = name;
            Description = description;
            Picture = picture;
            Public = isPublic;
            Validate();
        }

        private void Validate()
        {
            if (Math.Abs(Latitude) > 90)
                throw new ArgumentException("Invalid Latitude.");
            if (Math.Abs(Longitude) > 180)
                throw new ArgumentException("Invalid Longitude.");
            if (string.IsNullOrWhiteSpace(Name))
                throw new ArgumentException("Invalid Name.");
            if (string.IsNullOrWhiteSpace(Description))
                throw new ArgumentException("Invalid Name.");
            if (string.IsNullOrWhiteSpace(Picture))
                throw new ArgumentException("Invalid Name.");
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Latitude;
            yield return Longitude;
            yield return Name;
            yield return Description;
            yield return Picture;
            yield return Public;
        }
    }
}