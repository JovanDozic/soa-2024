namespace Explorer.Payments.API.Dtos
{
    public class CouponDto
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public double Discount { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public int? TourId { get; set; }
        public int AuthorId { get; set; }
    }
}