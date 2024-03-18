namespace Explorer.Encounters.Core.Domain.Utilities
{
    internal class DistanceCalculator
    {
        private const double EarthRadiusInKm = 6371.0;
        public static double CalculateDistance(Location pickedLocation, Location locationOfIterest)
        {
            double currentPositionLatInRad = locationOfIterest.Latitude * Math.PI / 180;
            double currentPointLatInRad = pickedLocation.Latitude * Math.PI / 180;

            double deltaLatInRad = Math.Abs(pickedLocation.Latitude - locationOfIterest.Latitude) * Math.PI / 180;
            double deltaLongInRad = Math.Abs(pickedLocation.Longitude - locationOfIterest.Longitude) * Math.PI / 180;

            double a = Math.Pow(Math.Sin(deltaLatInRad / 2), 2) +
                       Math.Cos(currentPointLatInRad) * Math.Cos(currentPositionLatInRad) *
                       Math.Pow(Math.Sin(deltaLongInRad / 2), 2);

            return 2 * EarthRadiusInKm * Math.Asin(Math.Sqrt(a));
        }
    }
}
