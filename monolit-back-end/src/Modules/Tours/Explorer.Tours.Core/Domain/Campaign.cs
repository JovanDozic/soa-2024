using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Tours.Core.Domain.Tours;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace Explorer.Tours.Core.Domain;
[JsonObject(MemberSerialization.OptIn)]
public class Campaign : JsonEntity
{
    [NotMapped]
    [JsonProperty]
    public List<Tour> Tours { get; private set; }
    [NotMapped]
    [JsonProperty]
    public string Title { get; private set; }
    [NotMapped]
    [JsonProperty]
    public int TouristId { get; private set; }
    [NotMapped]
    [JsonProperty]
    public int Difficult { get; private set; }
    [NotMapped]
    [JsonProperty]
    public float Length { get; private set; }
    public Campaign() { }

    [JsonConstructor]
    public Campaign(List<Tour> tours, int touristId, string title)
    {
        Tours = tours;
        TouristId = touristId;
        Title = title;
        Difficult = GetDifficulty();
        Length = GetLength();
        //Validate();
    }
    //private void Validate()
    //{
    //    if (Tours.Count < 2)
    //        throw new ArgumentException("2 Tours required");
    //    if (Difficult <= 0)
    //        throw new ArgumentException("Invalid diffcult");
    //    if (Length < 0)
    //        throw new ArgumentException("Invalid Length. Length can't be less then 0!");
    //}
    private int GetDifficulty()
    {
        var difficulty = 0;
        foreach(var tour in Tours)
        {
            difficulty += tour.Difficult;
        }
        return difficulty/Tours.Count;
    }
    private float GetLength()
    {
        float length = 0;
        foreach( var tour in Tours)
        {
            length += tour.Length;
        }
        return length;
    }
    public override void FromJson()
    {
        var campaign = JsonConvert.DeserializeObject<Campaign>(JsonObject ?? throw new NullReferenceException(
                                                               "Exception! No object to deserialize!")) ??
                       throw new NullReferenceException("Exception! Campaign is null!");
        Title = campaign.Title;
        Tours = campaign.Tours;
        Difficult = campaign.Difficult;
        TouristId = campaign.TouristId;
        Length = campaign.Length;
    }

    public override void ToJson()
    {
        var settings = new JsonSerializerSettings
        {
            Formatting = Formatting.Indented,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        };

        JsonObject = JsonConvert.SerializeObject(this, settings) ??
                         throw new JsonSerializationException("Exception! Could not serialize object!");
    }
}

