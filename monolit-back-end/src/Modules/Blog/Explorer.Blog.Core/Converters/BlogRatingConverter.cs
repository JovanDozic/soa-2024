using Explorer.Blog.API.Dtos;
using Explorer.Blog.Core.Domain;
using Vote = Explorer.Blog.API.Dtos.Vote;

namespace Explorer.Blog.Core.Converters
{
    public static class BlogRatingConverter
    {
        public static BlogRatingDto ToDto(this BlogRating blogRating)
        {
            if (blogRating == null)
            {
                return null;
            }
            return new BlogRatingDto
            {
                UserId = (int)blogRating.UserId,
                VotingDate = blogRating.VotingDate,
                Mark = (Vote)blogRating.Mark
            };
        }
        public static BlogRating ToDomain(this BlogRatingDto blogRatingDto)
        {
            return blogRatingDto == null ? null : 
                new BlogRating(blogRatingDto.UserId, blogRatingDto.VotingDate, (Blog.Core.Domain.Vote)blogRatingDto.Mark);
        }
    }

}
