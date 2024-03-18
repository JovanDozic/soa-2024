using Explorer.Tours.API.Dtos.TourExecutions;
using Explorer.Tours.Core.Domain.TourExecutions;

namespace Explorer.Tours.Core.Converters
{
    public static class PositionConverter
    {
        public static PositionDto ToDto(this Position position)
        {
            if (position == null) return null;
            return new PositionDto
            {
                Longitude = position.Longitude,
                Latitude = position.Latitude,
                LastActivity = position.LastActivity
            };
        }

        public static Position ToDomain(this PositionDto positionDto)
        {
            return positionDto == null ? null :
                new Position(positionDto.Latitude, positionDto.Longitude, positionDto.LastActivity);
        }
    }
}
