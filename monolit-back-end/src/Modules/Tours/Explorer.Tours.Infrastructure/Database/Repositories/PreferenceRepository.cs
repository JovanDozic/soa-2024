using Explorer.BuildingBlocks.Core.Domain;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Infrastructure.Database.Repositories
{
    public class PreferenceRepository : CrudDatabaseRepository<Preference, ToursContext>, IPreferenceRepository
    {
        private readonly ToursContext _context;
        private readonly DbSet<Preference> _dbSet;

        public PreferenceRepository(ToursContext dbContext) : base(dbContext)
        {
            _context = dbContext;
            _dbSet = DbContext.Set<Preference>();
        }

        public List<PreferenceDto> GetAllByTouristId(int touristId)
        {
            return _dbSet.Select(x => new PreferenceDto
            {
                Id = x.Id,
                UserId = x.UserId,
                Difficulty = x.Difficulty,
                Transport = x.Transport.ToString(),
                Tags = x.Tags
            })
            .Where(x => x.UserId == touristId)
            .ToList();
        }
    }
}
