using static Explorer.Stakeholders.API.Enums.UserEnums;

namespace Explorer.Stakeholders.API.Dtos
{
    public class UserDto
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; }
        public bool IsActive { get; set; }
        public List<FollowerDto> Followers { get; set; }
        public List<NotificationDto> Notifications { get; set; }
        public bool? IsBlogEnabled { get; set; }
    }
}
