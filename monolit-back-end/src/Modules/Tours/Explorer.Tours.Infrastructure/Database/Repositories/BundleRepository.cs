using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Tours.Core.Domain.Bundles;

namespace Explorer.Tours.Infrastructure.Database.Repositories
{
    public class BundleRepository : CrudDatabaseRepository<Bundle, ToursContext>
    {
        private readonly ToursContext _context;
        public BundleRepository(ToursContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }
    }
}
