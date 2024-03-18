using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using FluentResults;

namespace Explorer.Tours.Core.UseCases.Administration
{
    public class ObjectService : CrudService<ObjectDto, Domain.Object>, IObjectService
    {
        public readonly IObjectRepository _objectRepository;
        public ObjectService(ICrudRepository<Domain.Object> repository, IMapper mapper, IObjectRepository objectRepository) : base(repository, mapper)
        {
            _objectRepository = objectRepository;
        }

        public Result<List<ObjectDto>> GetAllPublicObjects()
        {
            return _objectRepository.GetAllPublicObjects();
        }
        public Result<ObjectDto> SetPublic(int objectId)
        {
            return _objectRepository.SetPublic(objectId);
        }
    }
}
