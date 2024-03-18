using System.Text.Json.Serialization;
using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Tours.Core.Domain.TourExecutions
{
    public class Position: ValueObject
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime LastActivity { get; set; }

        [JsonConstructor]
        public Position(double latitude, double longitude, DateTime lastActivity)
        {
            Validate(latitude, longitude);
            Latitude = latitude;
            Longitude = longitude;
            LastActivity = lastActivity;
        }


        public bool IsChanged(Position currentPosition)
        {
            return (Latitude != currentPosition.Latitude) || (Longitude != currentPosition.Longitude);
        }
        
        private static void Validate(double lat, double lng)
        {
            if (!(lat is > -90.0 and < 90.0))
                throw new ArgumentException("Exeception! Latitude is out of range!");
            if (!(lng is > -180.0 and < 180.0))
                throw new ArgumentException("Exeception! Longitude is out of range!");
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Latitude;
            yield return Longitude;
            yield return LastActivity;
        }
    }
}
