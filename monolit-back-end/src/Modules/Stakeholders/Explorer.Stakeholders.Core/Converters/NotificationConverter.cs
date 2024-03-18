using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.Core.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.Converters
{
    public static class NotificationConverter
    {
        public static NotificationDto ToDto(this Notification notification)
        {
            if (notification == null)
            {
                return null;
            }
            return new NotificationDto
            {
                NotificationId = notification.NotificationId,
                SenderId = (int)notification.SenderId,
                Message = notification.Message,
                Status = notification.Status,
                Timestamp = notification.Timestamp
            };
        }

        public static Notification ToDomain(this NotificationDto notificationDto)
        {
            return notificationDto == null ? null :
                new Notification(notificationDto.NotificationId, notificationDto.SenderId, notificationDto.Message, notificationDto.Status, notificationDto.Timestamp);
        }
    }
}