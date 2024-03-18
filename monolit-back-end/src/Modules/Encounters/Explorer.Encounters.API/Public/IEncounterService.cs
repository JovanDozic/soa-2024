using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.API.Dtos;
using FluentResults;

namespace Explorer.Encounters.API.Public
{
    public interface IEncounterService
    {
        Result<EncounterDto> Get(int id);
        Result<EncounterDto> Create(EncounterDto encounter);
        Result<PagedResult<EncounterDto>> GetPaged(int page, int pageSize);
        Result<EncounterDto> Update(EncounterDto encounter);
        Result Delete(int encounterId);
        Result<EncounterDto> Activate(int id, ParticipantLocationDto  participantLocation);
    }
}