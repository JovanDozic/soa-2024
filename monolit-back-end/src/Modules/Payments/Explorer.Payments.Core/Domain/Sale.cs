using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Tours.Core.Domain.Tours;
using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using JsonConstructorAttribute = Newtonsoft.Json.JsonConstructorAttribute;
using Explorer.Tours.API.Dtos.Tours;
using System.Diagnostics;
using System.Reflection.Metadata;
using System.Xml.Linq;
using static Explorer.Tours.API.Enums.TourEnums;

namespace Explorer.Payments.Core.Domain
{
    public class Sale : JsonEntity
    {
        [NotMapped]
        [JsonProperty]
        public List<TourDto>? ToursOnSale { get; set; } = new List<TourDto>();

        [NotMapped]
        [JsonProperty]
        public DateTime SaleStart { get; set; }

        [NotMapped]
        [JsonProperty]
        public DateTime SaleEnd { get; set; }

        [NotMapped]
        [JsonProperty]
        public int DiscountPercentage { get; set; }

        [NotMapped]
        [JsonProperty]
        public bool IsActive { get; set; }

        public Sale() { }

        [JsonConstructor]
        public Sale(List<TourDto> tours, DateTime start, DateTime end, int discount, bool isActive)
        {
            ToursOnSale = tours;
            SaleStart = start;
            SaleEnd = end;
            DiscountPercentage = discount;
            IsActive = isActive;
        }

        public void ActivateSale()
        {
            IsActive = true;
        }
        public override void FromJson()
        {
            var sale = JsonConvert.DeserializeObject<Sale>(JsonObject ??
                                                          throw new NullReferenceException(
                                                              "Exception! No object to deserialize!")) ??
                      throw new NullReferenceException("Exception! Tour is null!");

            ToursOnSale = sale.ToursOnSale;
            SaleStart = sale.SaleStart;
            SaleEnd = sale.SaleEnd;
            DiscountPercentage = sale.DiscountPercentage;
            IsActive = sale.IsActive;


        }

        public override void ToJson()
        {
            JsonObject = JsonConvert.SerializeObject(this, Formatting.Indented) ??
                         throw new JsonSerializationException("Exception! Could not serialize object!");
        }
    }
}
