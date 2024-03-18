using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using FluentResults;

namespace Explorer.Tours.API.Public.Administration
{
    public interface IBundleService
    {
        Result<BundleDto> Create(BundleDto dataIn);
        Result<BundleDto> Update(BundleDto dataIn);
        Result Delete(int id);
        Result<BundleDto> Publish(long id);
        Result<BundleDto> Archive(long id);
        Result<PagedResult<BundleDto>> GetPaged(int page, int pageSize);
    }
}
