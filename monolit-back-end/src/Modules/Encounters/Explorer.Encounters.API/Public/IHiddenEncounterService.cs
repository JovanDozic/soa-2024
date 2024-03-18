using Explorer.Encounters.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.API.Public
{
    public interface IHiddenEncounterService
    {
        Result<HiddenEncounterDto> Get(int id);
        Result<List<HiddenEncounterDto>> GetAll();
        Result<HiddenEncounterDto> Create(HiddenEncounterDto hiddenEncounter);
        Result<HiddenEncounterDto> Update(HiddenEncounterDto hiddenEncounter);
        Result<HiddenEncounterDto> Solve(int encounterId, ParticipantLocationDto location);
    }
}
