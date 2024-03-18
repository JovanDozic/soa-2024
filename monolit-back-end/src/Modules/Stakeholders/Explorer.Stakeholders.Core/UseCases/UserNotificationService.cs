using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain.Users;
using Explorer.Stakeholders.Core.Converters;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class UserNotificationService : CrudService<UserDto, User>, IUserNotificationService
    {
        public UserNotificationService(ICrudRepository<User> userRepository, IMapper mapper) : base(userRepository, mapper) { }

        public Result<List<NotificationDto>> GetNotifications(int userId)
        {
            var user = CrudRepository.Get(userId);

            if (user is null)
            {
                return Result.Fail<List<NotificationDto>>($"User not found ({FailureCode.NotFound}).");
            }

            try
            {
                var notifications = user.Notifications.ToList();
                List<NotificationDto> result = new List<NotificationDto>();

                foreach (var notification in notifications)
                {
                    result.Add(NotificationConverter.ToDto(notification));
                }

                return result;
            }
            catch (Exception ex)
            {
                return Result.Fail<List<NotificationDto>>(ex.Message);
            }
        }

        public long GetNextNotificationId(User user)
        {
            long maxId = user.Notifications.Any() ? user.Notifications.Max(n => n.NotificationId) : 0;
            return maxId + 1;
        }

        public Result NotifyFollowers(NotificationDto notificationDto)
        {
            var notification = NotificationConverter.ToDomain(notificationDto);
            var user = CrudRepository.Get(notification.SenderId);
            try
            {
                var followers = user.Followers.ToList();

                foreach (var follower in followers)
                {
                    var foundUser = CrudRepository.Get(follower.UserId);
                    foundUser.AddNotification(notification);
                    CrudRepository.Update(foundUser);
                }

                return Result.Ok();
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }

        public Result<UserDto> NotifyUser(int receiverId, NotificationDto notificationDto)
        {
            var notification = NotificationConverter.ToDomain(notificationDto);
            var user = CrudRepository.Get(receiverId);
            if (user is null)
            {
                return Result.Fail<UserDto>($"User not found ({FailureCode.NotFound}).");
            }
            try
            {
                user.AddNotification(notification);
                CrudRepository.Update(user);
                return MapToDto(user);
            }
            catch (Exception ex)
            {
                return Result.Fail<UserDto>(ex.Message);
            }
        }

        public Result<UserDto> ReadNotification(int userId, int notificationId)
        {
            var user = CrudRepository.Get(userId);
            if (user is null)
            {
                return Result.Fail<UserDto>($"User not found ({FailureCode.NotFound}).");
            }
            try
            {
                var notification = user.Notifications.FirstOrDefault(n => n.NotificationId == notificationId);
                if (notification != null)
                {
                    notification.MarkAsRead();
                    CrudRepository.Update(user);
                }
                return MapToDto(user);
            }
            catch (Exception ex)
            {
                return Result.Fail<UserDto>(ex.Message);
            }
        }

        public Result<UserDto> DeleteNotification(int userId, int notificationId)
        {
            var user = CrudRepository.Get(userId);
            if (user is null)
            {
                return Result.Fail<UserDto>($"User not found ({FailureCode.NotFound}).");
            }
            try
            {
                var notification = user.Notifications.FirstOrDefault(n => n.NotificationId == notificationId);
                if (notification != null)
                {
                    user.DeleteNotification(notification);
                    CrudRepository.Update(user);
                }
                return MapToDto(user);
            }
            catch (Exception ex)
            {
                return Result.Fail<UserDto>(ex.Message);
            }
        }
    }
}