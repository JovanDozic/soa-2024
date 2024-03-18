using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.UseCases.Administration
{
    public class PublicRegistrationRequestService : CrudService<PublicRegistrationRequestDto, PublicRegistrationRequest>, IPublicRegistrationRequestService
    {
        private readonly IPublicRegistrationRequestRepository _repository;

        public PublicRegistrationRequestService(ICrudRepository<PublicRegistrationRequest> repository, IMapper mapper, IPublicRegistrationRequestRepository requestRepository) : base(repository, mapper) 
        {
            _repository = requestRepository;
        }

        public Result<List<PublicRegistrationRequestDto>> GetAllPendingRequests()
        {
            return _repository.GetAllPendingRequests();
        }
    }
}
