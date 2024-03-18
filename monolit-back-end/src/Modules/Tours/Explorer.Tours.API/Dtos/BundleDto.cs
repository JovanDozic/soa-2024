using Explorer.Tours.API.Dtos.Tours;
using static Explorer.Tours.API.Enums.BundleEnums;

namespace Explorer.Tours.API.Dtos
{
    public class BundleDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public List<TourDto> Tours { get; set; }
        public BundleStatus Status { get; set; }
    }
}
