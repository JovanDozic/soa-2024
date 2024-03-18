using Explorer.BuildingBlocks.Core.Domain;
using Newtonsoft.Json;

namespace Explorer.Encounters.Core.Domain
{
    public class Location: ValueObject
    {
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        
        [JsonConstructor]
        public Location(double longitude, double latitude)
        {
            Validate(latitude, longitude);
            Longitude = longitude;
            Latitude = latitude;
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return new object[] { Longitude, Latitude };
        }

        private static void Validate(double latitude, double longitude)
        {
            if(90 < Math.Abs(latitude))  throw new ArgumentException("Latitude exceeds limit!");
            if(180 < Math.Abs(longitude))  throw new ArgumentException("Latitude exceeds limit!");
        }
    }
}
