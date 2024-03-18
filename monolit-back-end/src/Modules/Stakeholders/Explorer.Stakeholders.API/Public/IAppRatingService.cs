using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using FluentResults;

namespace Explorer.Stakeholders.API.Public
{
    public interface IAppRatingService
    {
        Result<PagedResult<AppRatingDto>> GetPaged(int page, int pageSize);
        Result<AppRatingDto> Create(AppRatingDto appRating);
        bool HasUserRated(int userId);
        // TODO: Uncomment
        //Result<AppRatingDto> Update(AppRatingDto appRating);
        //Result Delete(int id);
    }
}