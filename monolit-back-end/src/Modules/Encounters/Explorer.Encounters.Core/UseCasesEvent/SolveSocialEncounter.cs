using Explorer.Encounters.API.Dtos;
using Explorer.Encounters.Core.Domain.RepositoryInterfaces;
using Explorer.Encounters.Core.Domain.SolvingStrategies;
using Explorer.Encounters.Core.EventSourcingDomain;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.Core.UseCasesEvent
{
    public class SolveSocialEncounter
    {
        private ISocialEncounterRepository _socialEncounterRepository;

        public SolveSocialEncounter(ISocialEncounterRepository socialEncounterRepository)
        {
            _socialEncounterRepository = socialEncounterRepository;
        }

        public SocialEncounter Execute(int id, ParticipantLocationDto participantLocation)
        {
            try
            {
                var encounter = _socialEncounterRepository.Get(id);
                encounter.Solve(participantLocation.Username, participantLocation.Longitude, participantLocation.Latitude);

                _socialEncounterRepository.Update(encounter);
                return encounter;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
