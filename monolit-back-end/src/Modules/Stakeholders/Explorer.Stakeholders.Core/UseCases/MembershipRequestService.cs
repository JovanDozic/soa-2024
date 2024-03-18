using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Explorer.BuildingBlocks.Core.Domain;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using FluentResults;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class MembershipRequestService : CrudService<MembershipRequestDto, MembershipRequest>, IMembershipRequestService
    {
        
        public MembershipRequestService(ICrudRepository<MembershipRequest> crudRepository, IMapper mapper) : base(crudRepository, mapper)
        {
        }

        public Result<MembershipRequestDto> CreateMembershipRequest(MembershipRequestDto req)
        {
            try
            {
                var result = CrudRepository.Create(MapToDomain(req));
                return MapToDto(result);
            }
            catch (ArgumentException e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }

        public Result<MembershipRequestDto> AcceptMembershipRequest(int id)
        {
            var result = CrudRepository.Get(id);
            result.IsAccepted = true;
            CrudRepository.Update(result);
            return MapToDto(result);

        }

        public Result<MembershipRequestDto> RejectMembershipRequest(int id)
        {
            var result = CrudRepository.Get(id);
            result.IsAccepted = false;
            CrudRepository.Update(result);
            return MapToDto(result);
        } 
    }
}
