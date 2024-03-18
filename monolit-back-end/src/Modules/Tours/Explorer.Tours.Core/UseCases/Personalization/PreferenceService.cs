using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Personalization;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.UseCases.Personalization
{
    public class PreferenceService : CrudService<PreferenceDto, Preference>, IPreferenceService
    {
        private readonly IPreferenceRepository _preferenceRepository;
        public PreferenceService(ICrudRepository<Preference> repository, IMapper mapper, IPreferenceRepository preferenceRepository) : base(repository, mapper)
        {
            _preferenceRepository = preferenceRepository;
        }

        public Result<List<PreferenceDto>> GetAllForTourist(int touristId)
        {
            return _preferenceRepository.GetAllByTouristId(touristId);
        }

    }
}
