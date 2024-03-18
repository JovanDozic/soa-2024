using Explorer.Tours.API.Dtos.Tours;
using Explorer.Tours.Core.Domain.Tours;

namespace Explorer.Tours.Core.Converters
{
    public static class PointConverter
    {
        public static PointDto ToDto(this Point point)
        {
            if (point == null) return null;
            return new PointDto
            {
                Description = point.Description,
                Latitude = point.Latitude,
                Longitude = point.Longitude,
                Name = point.Name,
                Picture = point.Picture,
                Public = point.Public,
            };
        }

        public static Point ToDomain(this PointDto pointDto)
        {
            return pointDto == null ? null :
                new Point(pointDto.Latitude, pointDto.Longitude, pointDto.Name, pointDto.Description, pointDto.Picture, pointDto.Public);
        }
    }
}
