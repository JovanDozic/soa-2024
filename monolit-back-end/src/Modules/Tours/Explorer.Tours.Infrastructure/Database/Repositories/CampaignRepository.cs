using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Tours.API.Public;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;

namespace Explorer.Tours.Infrastructure.Database.Repositories
{
    public class CampaignRepository : CrudDatabaseRepository<Campaign, ToursContext>, ICampaignRepository
    {
        private readonly ToursContext _context;
        public CampaignRepository(ToursContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }
    }
}
