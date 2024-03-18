using Explorer.Tours.Core.Domain.TourExecutions;

namespace Explorer.Tours.Core.Domain.Utilities
{
    internal class DistanceCalculator
    {
        private const double EarthRadiusInKm = 6371.0;
        public static double CalculateDistance(PointTask currentPoint, Position currentPosition)
        {
            double currentPositionLatInRad = currentPosition.Latitude * Math.PI / 180;
            double currentPointLatInRad = currentPoint.Point.Latitude * Math.PI / 180;

            double deltaLatInRad = Math.Abs(currentPoint.Point.Latitude - currentPosition.Latitude) * Math.PI / 180;
            double deltaLongInRad = Math.Abs(currentPoint.Point.Longitude - currentPosition.Longitude) * Math.PI / 180;

            double a = Math.Pow(Math.Sin(deltaLatInRad / 2), 2) +
                       Math.Cos(currentPointLatInRad) * Math.Cos(currentPositionLatInRad) *
                       Math.Pow(Math.Sin(deltaLongInRad / 2), 2);

            return 2 * EarthRadiusInKm * Math.Asin(Math.Sqrt(a));
        }
    }
}
