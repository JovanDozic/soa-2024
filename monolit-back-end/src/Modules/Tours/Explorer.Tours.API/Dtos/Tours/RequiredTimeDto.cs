using static Explorer.Tours.API.Enums.TourEnums;

namespace Explorer.Tours.API.Dtos.Tours
{
    public class RequiredTimeDto
    {
        public TransportType TransportType { get; set; }
        public int Minutes { get; set; }
    }
}
