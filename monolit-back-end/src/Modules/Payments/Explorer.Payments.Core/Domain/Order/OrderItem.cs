using System.Text.Json.Serialization;
using Explorer.BuildingBlocks.Core.Domain;
using static Explorer.Payments.API.Enums.OrderItemEnums;

namespace Explorer.Payments.Core.Domain.Order
{
    public class OrderItem : ValueObject
    {
        public OrderItemType Type { get; set; }
        public int IdType { get; init; }
        public List<TourInfo>? ToursInfo { get; init; }
        public string Name { get; init; }
        public double Price { get; set; }
        public string Image { get; init; }
        public string? CouponCode { get; init; }


        [JsonConstructor]
        public OrderItem(int idType, string name, double price, string image, string couponCode )

        {
            IdType = idType;
            Name = name;
            Price = price;
            Image = image;
            CouponCode = couponCode;
            Validate();
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return IdType;
            yield return Name;
            yield return Price;
            yield return Image;
            yield return CouponCode; // Uključeno kod kupona u proveru jednakosti
        }

        private void Validate()
        {
            if (string.IsNullOrEmpty(Name)) throw new ArgumentException("Invalid name");
            if (Price < 0) throw new ArgumentException("Invalid price");
            if (IdType == 0) throw new ArgumentException("Invalid TypeId");
            if (Image == string.Empty || Image == null) throw new ArgumentException("Invalid image");
            //if (string.IsNullOrEmpty(CouponCode) )
             //   throw new ArgumentException("Invalid coupon code");
        }
    }

    }

