using static Explorer.Payments.API.Enums.OrderItemEnums;

namespace Explorer.Payments.API.Dtos
{
    public class OrderItemDto
    {
        public string Type { get; set; }
        public int IdType { get; set; }
        public List<TourInfoDto>? ToursInfo { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Image { get; set; }
        public string? CouponCode { get; set; }  

    }
}
