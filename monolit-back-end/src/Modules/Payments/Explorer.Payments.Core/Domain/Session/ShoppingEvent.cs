using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static Explorer.Payments.API.Enums.ShoppingEventEnums;

namespace Explorer.Payments.Core.Domain.Session
{
    public class ShoppingEvent : ValueObject
    {
        public EventType EventType { get; init; }
        public long? ItemId { get; init; }
        public DateTime Timestamp { get; init; }

        [JsonConstructor]
        public ShoppingEvent(EventType eventType, DateTime timestamp)
        {
            EventType = eventType;
            Timestamp = timestamp;
        }
        public ShoppingEvent(EventType eventType, long itemId, DateTime timestamp)
        {
            EventType = eventType;
            Timestamp = timestamp;
            ItemId = itemId;
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return EventType;
            yield return ItemId; //Ako bude greska mzd ovde?
            yield return Timestamp;
        }
    }
}
