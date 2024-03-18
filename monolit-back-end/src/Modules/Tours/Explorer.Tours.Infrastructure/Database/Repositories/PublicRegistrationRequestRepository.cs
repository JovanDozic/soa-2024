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
    public class PublicRegistrationRequestRepository : CrudDatabaseRepository<PublicRegistrationRequest, ToursContext>, IPublicRegistrationRequestRepository
    {
        private readonly ToursContext _context;
        private readonly DbSet<PublicRegistrationRequest> _dbSet;
        public PublicRegistrationRequestRepository(ToursContext dbContext) : base(dbContext)
        {
            _context = dbContext;
            _dbSet = DbContext.Set<PublicRegistrationRequest>();
        }

        public List<PublicRegistrationRequestDto> GetAllPendingRequests()
        {
            return _dbSet
            .Where(x => x.Status == RequestStatus.Pending)
            .Select(x => new PublicRegistrationRequestDto
            {
                Id = x.Id,
                ObjectId = x.ObjectId,
                ObjectName = x.ObjectName,
                TourId = x.TourId,
                PointName = x.PointName,
                Comment = x.Comment,
                Status = x.Status.ToString(),
            })
            .ToList();
        }
    }
}
