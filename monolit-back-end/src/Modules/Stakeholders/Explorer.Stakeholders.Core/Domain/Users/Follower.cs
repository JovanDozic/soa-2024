using Explorer.BuildingBlocks.Core.Domain;
using System.Text.Json.Serialization;

namespace Explorer.Stakeholders.Core.Domain.Users
{
    public class Follower : Entity
    {
        [JsonPropertyName("UserId")]
        public long UserId => base.Id;
        [JsonPropertyName("Username")]
        public string Username { get; private set; }
        [JsonPropertyName("Date")]
        public DateTime Date { get; private set; }

        [JsonConstructor]
        public Follower(long userId, string username, DateTime date)
        {
            Id = userId;
            Username = username;
            Date = date;
        }
    }
}
