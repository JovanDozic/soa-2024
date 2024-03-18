using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static Explorer.Stakeholders.API.Enums.NotificationEnums;

namespace Explorer.Stakeholders.Core.Domain.Users
{
    public class Notification : Entity
    {
        [JsonPropertyName("NotificationId")]
        public long NotificationId => base.Id;
        [JsonPropertyName("SenderId")]
        public long SenderId { get; private set; }
        [JsonPropertyName("Message")]
        public string Message { get; private set; }
        [JsonPropertyName("Status")]
        public NotificationStatus Status { get; private set; }
        [JsonPropertyName("Timestamp")]
        public DateTime Timestamp { get; private set; }

        [JsonConstructor]
        public Notification(long notificationId, long senderId, string message, NotificationStatus status, DateTime timestamp)
        {
            Id = notificationId;
            SenderId = senderId;
            Message = message;
            Status = status;
            Timestamp = timestamp;
        }

        public void MarkAsRead()
        {
            Status = NotificationStatus.Read;
        }
    }
}