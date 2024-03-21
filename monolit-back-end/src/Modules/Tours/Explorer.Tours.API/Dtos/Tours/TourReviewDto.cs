
namespace Explorer.Tours.API.Dtos.Tours
{
    public class TourReviewDto
    {
        public int Rating { get; set; }
        public string Comment { get; set; }
        public int TouristId { get; set; }
        public string TouristUsername { get; set; }
        public DateTime TourDate { get; set; }
        public DateTime CreationDate { get; set; }
        //public List<string> Images { get; set; }
    }
}