﻿
namespace Explorer.Tours.API.Dtos
{
    public class TouristPositionDto
    {
        public int Id { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public int TouristId { get; set; }
    }
}
