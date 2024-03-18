using Explorer.BuildingBlocks.Core.Domain;
using System;

namespace Explorer.Payments.Core.Domain
{
    public class Coupon : Entity
    {
       
        public string Code { get; set; }
        public double Discount { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public int TourId { get; set; }
        public int AuthorId { get; set; }

        public Coupon()
        {
       
        }
        public Coupon(string code, double discount, DateTime? expiryDate, int tourId, int authorId)
        {
            Code = code;
            Discount = discount;
            ExpiryDate = expiryDate;
            TourId = tourId;
            AuthorId = authorId;
            Validate();
        }

        
        private void Validate()
        {
            if (string.IsNullOrEmpty(Code))
                throw new ArgumentException("Invalid coupon code");

            if (Discount < 0)
                throw new ArgumentException("Invalid discount value");

            if (TourId <= 0)
                throw new ArgumentException("Invalid TourId");

            if (AuthorId <= 0)
                throw new ArgumentException("Invalid AuthorId");

            if (ExpiryDate.HasValue && ExpiryDate.Value < DateTime.Now)
                throw new ArgumentException("Coupon has expired");
        }
    }
}
