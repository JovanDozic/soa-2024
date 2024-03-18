using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Tours.Core.Domain.Tours;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using static Explorer.Tours.API.Enums.BundleEnums;
using static Explorer.Tours.API.Enums.TourEnums;

namespace Explorer.Tours.Core.Domain.Bundles
{
    public class Bundle : JsonEntity
    {
        [NotMapped][JsonProperty]
        public string? Name { get; private set; }
        [NotMapped][JsonProperty]
        public double? Price { get; private set; }
        [NotMapped][JsonProperty]
        public List<Tour>? Tours { get; private set; }
        [NotMapped]
        [JsonProperty]
        public BundleStatus Status { get; private set; }

        public Bundle()
        {

        }

        [JsonConstructor]
        public Bundle(string? name, double? price, List<Tour>? tours)
        {
            Name = name;
            Price = price;
            Tours = tours;
            Validate();
        }

        public void PublishBundle()
        {
            var publishedTours = 0;
            foreach(var tour in Tours)
            {
                if(tour.Status == TourStatus.Published) 
                {
                    publishedTours++;
                }
            }

            if(publishedTours >= 2)
            {
                Status = BundleStatus.Published;
                return;
            }

            throw new ArgumentException("You need minimum two public tours in bundle to publish it");
        }

        public void ArchiveBundle()
        {
            if(Status == BundleStatus.Published)
            {
                Status = BundleStatus.Archived;
                return;
            }

            throw new ArgumentException("Bundle needs to be published first");
        }

        private void Validate()
        {
            if(string.IsNullOrEmpty(Name))
                throw new ArgumentNullException("Invalid name");
            if (Price < 0)
                throw new ArgumentOutOfRangeException("Invalid price");
        }


        public override void FromJson()
        {
            var bundle = JsonConvert.DeserializeObject<Bundle>(JsonObject ??
                                                          throw new NullReferenceException(
                                                              "Exception! No object to deserialize!")) ??
                      throw new NullReferenceException("Exception! Tour is null!");

            Name = bundle.Name;
            Price = bundle.Price;
            Tours = bundle.Tours;
            Status = bundle.Status;
        }

        public override void ToJson()
        {
            JsonObject = JsonConvert.SerializeObject(this, Formatting.Indented) ??
                         throw new JsonSerializationException("Exception! Could not serialize object!");
        }
    }
}
