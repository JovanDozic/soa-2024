using Explorer.Tours.API.Dtos.Tours;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace Explorer.Tours.API.Dtos;
public class CampaignDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public List<TourDto> Tours { get; set; }
    public int TouristId { get; set; }
    public int Difficult { get; set; }
    public float Length { get; set; }
}