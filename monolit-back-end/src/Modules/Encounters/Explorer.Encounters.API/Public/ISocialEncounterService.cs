using Explorer.Encounters.API.Dtos;
using FluentResults;

namespace Explorer.Encounters.API.Public
{
    public interface ISocialEncounterService
    {
        Result<SocialEncounterDto> Get(int id);
        Result<List<SocialEncounterDto>> GetAll();
        Result<SocialEncounterDto> Create(SocialEncounterDto socialEncounter);
        Result<SocialEncounterDto> Update(SocialEncounterDto socialEncounter);
        Result<SocialEncounterDto> Activate(int id, ParticipantLocationDto participantLocation);
        Result<SocialEncounterDto> Solve(int id, ParticipantLocationDto participantLocation);
    }
}
