using Explorer.BuildingBlocks.Core.Domain;
using System.Text.Json.Serialization;

namespace Explorer.Payments.Core.Domain
{
    public class TourPurchaseToken : Entity
    {
        [JsonPropertyName("Id")]
        public long TokenId => base.Id;
        public int UserId { get; init; }
        public int TourId { get; init; }
        public DateTime PurchaseTime { get; init; }
        public string TourName { get; init; }
        public string TourImage { get; init; }

        public TourPurchaseToken(long id, int userId, int tourId, DateTime purchaseTime, string tourName, string tourImage)
        {
            Id = id;
            UserId = userId;
            TourId = tourId;
            PurchaseTime = purchaseTime;
            TourName = tourName;
            TourImage = tourImage;
            Validate();
        }

        private void Validate()
        {
            if (UserId == 0) throw new ArgumentOutOfRangeException("Unauthenticated user!");
            if (TourId == 0) throw new ArgumentOutOfRangeException();
            if (TourName == string.Empty || TourName == null) throw new ArgumentNullException();
            if (TourImage == string.Empty || TourImage == null) throw new ArgumentNullException();
        }
    }
}
