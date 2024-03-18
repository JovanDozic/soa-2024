using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Explorer.Stakeholders.API.Enums.NotificationEnums;

namespace Explorer.Stakeholders.API.Dtos
{
    public class NotificationDto
    {
        public long NotificationId { get; set; }
        public long SenderId { get; set; }
        public string Message { get; set; }
        public NotificationStatus Status { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
