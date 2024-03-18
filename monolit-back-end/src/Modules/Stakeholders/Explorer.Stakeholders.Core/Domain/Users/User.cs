using Explorer.BuildingBlocks.Core.Domain;
using static Explorer.Stakeholders.API.Enums.NotificationEnums;
using static Explorer.Stakeholders.API.Enums.UserEnums;

namespace Explorer.Stakeholders.Core.Domain.Users;

public class User : Entity
{
    public string Username { get; private set; }
    public string Password { get; private set; }
    public UserRole Role { get; private set; }
    public string Email { get; private set; }
    public bool IsActive { get; set; }
    public List<Follower>? Followers { get; private set; }
    public List<Notification>? Notifications { get; private set; }
    public bool IsProfileActivated { get; set; }
    public bool? IsBlogEnabled { get; set; }
    public User() { }
    public User(string username, string password, UserRole role, bool isActive, string email, List<Follower> followers, List<Notification>? notifications, bool isProfileActivated)
    {
        Validate(username, password);
        Username = username;
        Role = role;
        IsActive = isActive;
        Followers = followers;
        Notifications = notifications;
        Email = email;
        IsProfileActivated = isProfileActivated;
        SecurePassword(password);
    }
    private void Validate(string username, string password)
    {
        if (string.IsNullOrWhiteSpace(username)) throw new ArgumentException("Invalid Name");
        if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Invalid Password");
    }

    public string GetPrimaryRoleName()
    {
        return Role.ToString().ToLower();
    }

    public void AddFollower(User follower)
    {
        if (follower is null) throw new ArgumentNullException(nameof(follower));
        if (!Followers.Any(f => f.Id == follower.Id))
        {
            Followers.Add(new(follower.Id, follower.Username, DateTime.Now));
        }
    }

    public void RemoveFollower(User follower)
    {
        if (follower is null) throw new ArgumentNullException(nameof(follower));
        if (Followers.Any(f => f.Id == follower.Id))
        {
            Followers.Remove(new(follower.Id, follower.Username, DateTime.Now));
        }
    }
    public long GetNextNotificationId()
    {
        long maxId = Notifications.Any() ? Notifications.Max(n => n.NotificationId) : 0;
        return maxId + 1;
    }

    public void AddNotification(Notification notification)
    {
        if (notification is null) throw new ArgumentNullException(nameof(notification));
        long notificationId = GetNextNotificationId();
        Notifications.Add(new(notificationId, notification.SenderId, notification.Message, NotificationStatus.Unread, DateTime.Now));
    }

    public void DeleteNotification(Notification notification)
    {
        if (notification is null) throw new ArgumentNullException(nameof(notification));
        if (Notifications.Any(n => n.Id == notification.Id))
        {
            Notifications.Remove(new(notification.Id, notification.SenderId, notification.Message, NotificationStatus.Unread, DateTime.Now));
        }
    }

    public bool ChangePassword(string newPassword)
    {
        if (VerifyPassword(newPassword) || string.IsNullOrEmpty(newPassword))
            return false;
        SecurePassword(newPassword);
        return true;
    }
    public bool VerifyPassword(string password)
    {
        return BCrypt.Net.BCrypt.Verify(password, Password);
    }
    private void SecurePassword(string password)
    {
        Password = BCrypt.Net.BCrypt.HashPassword(password, BCrypt.Net.BCrypt.GenerateSalt(11));
    }
}
