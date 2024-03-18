using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using FluentResults;


namespace Explorer.Tours.API.Public.Administration;

public interface IObjectService
{
    Result<PagedResult<ObjectDto>> GetPaged(int page, int pageSize);
    Result<ObjectDto> Create(ObjectDto objects);
    Result<ObjectDto> Update(ObjectDto objects);
    Result Delete(int id);
    Result<List<ObjectDto>> GetAllPublicObjects();
    Result<ObjectDto> SetPublic(int objectId);
}

