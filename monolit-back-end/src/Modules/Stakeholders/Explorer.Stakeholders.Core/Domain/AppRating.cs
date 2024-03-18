using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Stakeholders.Core.Domain
{
    public class AppRating : Entity
    {
        public int UserId { get; init; }
        public int Rating { get; init; }
        public string? Description { get; init; }
        public DateTime DateCreated { get; init; }

        public AppRating(int userId, int rating, string? description, DateTime dateCreated)
        {
            UserId = userId;

            if (rating < 1) rating = 1;
            else if (rating > 5) rating = 5;
            Rating = rating;

            if (string.IsNullOrEmpty(description)) description = null;
            Description = description;

            DateCreated = dateCreated;
        }
    }
}
