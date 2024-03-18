namespace Explorer.Tours.API.Dtos.Tours
{
    public class PointDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Picture { get; set; }
        public bool Public { get; set; }
    }
}
