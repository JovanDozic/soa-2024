using Explorer.Tours.API.Dtos;
using FluentResults;

namespace Explorer.Tours.API.Public.Administration
{
    public interface ITouristPositionService
    {
        Result<TouristPositionDto> Update(TouristPositionDto touristPositionDto);
    }
}
