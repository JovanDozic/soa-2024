using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers
{
    [Authorize(Policy = "allRolesPolicy")]
    [Route("api/notifications")]
    public class UserNotificationController : BaseApiController
    {
        private readonly IUserNotificationService _userNotificationService;

        public UserNotificationController(IUserNotificationService userNotificationService)
        {
            _userNotificationService = userNotificationService;
        }

        [HttpGet("{userId:int}")]
        public ActionResult<List<NotificationDto>> GetNotifications(int userId)
        {
            var result = _userNotificationService.GetNotifications(userId);
            return CreateResponse(result);
        }

        [HttpPatch]
        public ActionResult NotifyFollowers([FromBody] NotificationDto notificationDto)
        {
            var result = _userNotificationService.NotifyFollowers(notificationDto);
            return CreateResponse(result);
        }

        [HttpPatch("{receiverId:int}")]
        public ActionResult<UserDto> NotifyUser(int receiverId, [FromBody] NotificationDto notificationDto)
        {
            var result = _userNotificationService.NotifyUser(receiverId, notificationDto);
            return CreateResponse(result);
        }

        [HttpPatch("user/{userId:int}/status/{notificationId:int}")]
        public ActionResult<UserDto> ReadNotification(int userId, int notificationId)
        {
            var result = _userNotificationService.ReadNotification(userId, notificationId);
            return CreateResponse(result);
        }

        [HttpPatch("user/{userId:int}/delete/{notificationId}")]
        public ActionResult<UserDto> DeleteNotification(int userId, int notificationId)
        {
            var result = _userNotificationService.DeleteNotification(userId, notificationId);
            return CreateResponse(result);
        }
    }
}