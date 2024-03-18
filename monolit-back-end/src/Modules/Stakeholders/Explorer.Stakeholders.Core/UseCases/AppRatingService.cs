using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using FluentResults;

namespace Explorer.Stakeholders.Core.UseCases;

public class AppRatingService : CrudService<AppRatingDto, AppRating>, IAppRatingService
{
    public AppRatingService(ICrudRepository<AppRating> repository, IMapper mapper) : base(repository, mapper)
    {
    }

    public bool HasUserRated(int userId)
    {
        Result<PagedResult<AppRatingDto>> allRatingsResult = GetPaged(1, int.MaxValue);

        if (allRatingsResult.IsSuccess)
        {
            var allRatings = allRatingsResult.Value.Results;
            return allRatings.Any(rating => rating.UserId == userId);
        }

        return false;
    }


}
