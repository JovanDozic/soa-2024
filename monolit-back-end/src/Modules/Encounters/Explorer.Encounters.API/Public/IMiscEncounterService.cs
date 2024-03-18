using Explorer.Encounters.API.Dtos;
using FluentResults;

namespace Explorer.Encounters.API.Public
{
    public interface IMiscEncounterService
    {
        Result<MiscEncounterDto> Get(int id);
        Result<List<MiscEncounterDto>> GetAll();
        Result<MiscEncounterDto> Create(MiscEncounterDto miscEncounter);
        Result<MiscEncounterDto> Update(MiscEncounterDto miscEncounter);
        Result<MiscEncounterDto> Solve(int id, string participantUsername);
    }
}
