
namespace Explorer.Tours.API.Dtos
{
    public enum Category { WC, RESTAURANT, PARKING, OTHER };
    public class ObjectDto
    {
        public long Id { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Picture { get; set; }
        public string Category { get; set; }
        public bool Public { get; set; }

    }
}
