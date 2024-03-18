using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain.Bundles;
using FluentResults;

namespace Explorer.Tours.Core.UseCases.Administration
{
    public class BundleService : CrudService<BundleDto, Bundle>, IBundleService
    {
        public BundleService(ICrudRepository<Bundle> crudRepository, IMapper mapper) : base(crudRepository, mapper)
        {
        }

        public Result<BundleDto> Archive(long id)
        {
            var bundleDb = CrudRepository.Get(id);
            bundleDb.ArchiveBundle();
            CrudRepository.Update(bundleDb);
            return MapToDto(bundleDb);
        }

        public Result<BundleDto> Publish(long id)
        {
            var bundleDb = CrudRepository.Get(id);
            bundleDb.PublishBundle();
            CrudRepository.Update(bundleDb);
            return MapToDto(bundleDb);
        }


    }
}
