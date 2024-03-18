using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Public.Personalization
{
    public interface IPreferenceService
    {
        Result<PreferenceDto> Create(PreferenceDto preference);
        Result<PagedResult<PreferenceDto>> GetPaged(int page, int pageSize);
        Result<PreferenceDto> Get(int id);
        Result<PreferenceDto> Update(PreferenceDto preference);
        Result Delete(int id);
        Result<List<PreferenceDto>> GetAllForTourist(int touristId);
    }
}
