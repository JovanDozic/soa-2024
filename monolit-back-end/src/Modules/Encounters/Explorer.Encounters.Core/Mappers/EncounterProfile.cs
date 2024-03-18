using AutoMapper;
using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Encounters.API.Dtos;
using Explorer.Encounters.Core.Domain;
using Explorer.Encounters.Core.Domain.Participants;
using Explorer.Encounters.Core.Domain.SolvingStrategies;
using Explorer.Encounters.Core.EventSourcingDomain;

namespace Explorer.Encounters.Core.Mappers
{
    public class EncounterProfile: Profile
    {
        public EncounterProfile()
        {
            CreateMap<LocationDto, Location>().ReverseMap();
            CreateMap<ParticipantDto, Participant>().ReverseMap();
            CreateMap<CompleterDto, Completer>().ReverseMap();
            CreateMap<EncounterDto, Encounter>().ReverseMap();
            CreateMap<SocialEncounterDto, SocialEncounter>().ReverseMap();
            CreateMap<HiddenEncounterDto, HiddenEncounter>().ReverseMap();
            CreateMap<MiscEncounterDto, MiscEncounter>().ReverseMap();
            CreateMap<SocialEncounterEventDto, SocialEncounterEvent>().ReverseMap();
        }
    }
}
