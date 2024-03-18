using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.API.Dtos;
using Explorer.Encounters.API.Enums;
using Explorer.Encounters.API.Public;
using Explorer.Encounters.Core.Domain;
using FluentResults;

namespace Explorer.Encounters.Core.UseCases
{
    public class EncounterService: CrudService<EncounterDto, Encounter>, IEncounterService
    {
        public EncounterService(ICrudRepository<Encounter> crudRepository, IMapper mapper) : base(crudRepository, mapper)
        {
        }

        public Result<EncounterDto> Activate(int id, ParticipantLocationDto participantLocation)
        {
            var encounter = CrudRepository.Get(id);
            if (encounter.Status != EncounterStatus.Active) return Result.Fail(FailureCode.Conflict);
            var result = encounter.Activate(participantLocation.Username, participantLocation.Longitude, participantLocation.Latitude);
            if (result) CrudRepository.Update(encounter);
            return MapToDto(encounter);
        }
    }
}
