using Explorer.Stakeholders.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Public
{
    public interface IUserNotificationService
    {
        public Result<List<NotificationDto>> GetNotifications(int userId);
        public Result NotifyFollowers(NotificationDto notification);
        public Result<UserDto> NotifyUser(int receiverId, NotificationDto notification);
        public Result<UserDto> ReadNotification(int userId, int notificationId);
        public Result<UserDto> DeleteNotification(int userId, int notificationId);
    }
}