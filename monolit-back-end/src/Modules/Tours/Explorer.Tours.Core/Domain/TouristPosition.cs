using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Tours.Core.Domain;
public class TouristPosition : Entity
{
    public double Longitude { get; init; }
    public double Latitude { get; init; }
    public int TouristId { get; init; }

    public TouristPosition(double longitude, double latitude, int touristId)
    {
        if (Math.Abs(latitude) > 90) throw new ArgumentException("Invalid Latitude.");
        if (Math.Abs(longitude) > 180) throw new ArgumentException("Invalid Longitude.");
        if (touristId == 0 ) throw new ArgumentException("Invalid tourist ID!");
        Longitude = longitude;
        Latitude = latitude;
        TouristId = touristId;
    }
}

