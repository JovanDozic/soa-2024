using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.API.Dtos;
using Explorer.Encounters.API.Public;
using Explorer.Encounters.Core.Domain.RepositoryInterfaces;
using Explorer.Encounters.Core.Domain.SolvingStrategies;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.Core.UseCases
{
    public class HiddenEncounterService : BaseService<HiddenEncounterDto, HiddenEncounter>, IHiddenEncounterService
    {
        private readonly IHiddenEncounterRepository _repository;

        public HiddenEncounterService(IMapper mapper, IHiddenEncounterRepository repository) : base(mapper)
        {
            _repository = repository;
        }

        public Result<HiddenEncounterDto> Create(HiddenEncounterDto hiddenEncounter)
        {
            var result = _repository.Create(MapToDomain(hiddenEncounter));
            return MapToDto(result);
        }

        public Result<HiddenEncounterDto> Get(int id)
        {
            var encounter = _repository.Get(id);
            return MapToDto(encounter);
        }

        public Result<List<HiddenEncounterDto>> GetAll()
        {
            var encounters = _repository.GetAll();
            return MapToDto(encounters);
        }

        public Result<HiddenEncounterDto> Update(HiddenEncounterDto hiddenEncounter)
        {
            try
            {
                var result = _repository.Update(MapToDomain(hiddenEncounter));
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

        public Result<HiddenEncounterDto> Solve(int encounterId, ParticipantLocationDto location)
        {
            var encounter = _repository.Get(encounterId);
            encounter.Solve(location.Username, location.Longitude, location.Latitude);
            _repository.Update(encounter);
            return MapToDto(encounter);
        }
    }
}
