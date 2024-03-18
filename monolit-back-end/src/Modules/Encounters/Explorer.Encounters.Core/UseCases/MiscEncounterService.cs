using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.API.Dtos;
using Explorer.Encounters.API.Public;
using Explorer.Encounters.Core.Domain.RepositoryInterfaces;
using Explorer.Encounters.Core.Domain.SolvingStrategies;
using Explorer.Stakeholders.API.Internal;
using FluentResults;

namespace Explorer.Encounters.Core.UseCases
{
    public class MiscEncounterService : BaseService<MiscEncounterDto, MiscEncounter>, IMiscEncounterService
    {
        private readonly IMiscEncounterRepository _miscEncounterRepository;
        private readonly IInternalPersonService _personService;

        public MiscEncounterService(IMiscEncounterRepository repository, IMapper mapper, IInternalPersonService personService) : base(mapper)
        {
            _miscEncounterRepository = repository;
            _personService = personService;
        }

        public Result<MiscEncounterDto> Get(int id)
        {
            var encounter = _miscEncounterRepository.Get(id);
            return MapToDto(encounter);
        }

        public Result<List<MiscEncounterDto>> GetAll()
        {
            var encounters = _miscEncounterRepository.GetAll();
            return MapToDto(encounters);
        }

        public Result<MiscEncounterDto> Create(MiscEncounterDto miscEncounter)
        {
            var result = _miscEncounterRepository.Create(MapToDomain(miscEncounter));
            return MapToDto(result);
        }

        public Result<MiscEncounterDto> Update(MiscEncounterDto miscEncounter)
        {
            try
            {
                var result = _miscEncounterRepository.Update(MapToDomain(miscEncounter));
                return MapToDto(result);
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
            catch (ArgumentException e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }

        public Result<MiscEncounterDto> Solve(int id, string participantUsername)
        {
            var encounter = _miscEncounterRepository.Get(id);
            var completers = encounter.Solve(participantUsername);
            if (completers.Count > 0) _personService.RewardWithXp(completers.Select(u => u.Username).ToList(), encounter.Experience);
            _miscEncounterRepository.Update(encounter);
            return MapToDto(encounter);
        }
    }
}
