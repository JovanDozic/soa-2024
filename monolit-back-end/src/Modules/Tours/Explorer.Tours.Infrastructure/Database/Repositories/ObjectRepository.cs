using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Dtos.Tours;
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
    public class ObjectRepository : CrudDatabaseRepository<Core.Domain.Object, ToursContext>, IObjectRepository
    {
        private readonly ToursContext _context;
        private readonly DbSet<Core.Domain.Object> _dbSet;

        public ObjectRepository(ToursContext dbContext) : base(dbContext)
        {
            _context = dbContext;
            _dbSet = DbContext.Set<Core.Domain.Object>();
        }

        public List<ObjectDto> GetAllPublicObjects()
        {
            return _dbSet
            .Where(x => x.Public == true)
            .Select(x => new ObjectDto
            {
                Id = x.Id,
                Longitude = x.Longitude,
                Latitude = x.Latitude,
                Name = x.Name,
                Description = x.Description,    
                Picture = x.Picture,
                Category = x.Category.ToString(),
                Public = x.Public,
            })
            .ToList();
        }

        public ObjectDto SetPublic(int objectId)
        {
            var existingObject = _context.Objects.FirstOrDefault(x => x.Id == objectId);

            if (existingObject != null)
            {
                existingObject.Public = true;
                _context.Update(existingObject);
                _context.SaveChanges();
            }

            return new ObjectDto
            {
                Id = existingObject.Id,
                Longitude = existingObject.Longitude,
                Latitude = existingObject.Latitude,
                Name = existingObject.Name,
                Description = existingObject.Description,
                Picture = existingObject.Picture,
                Category = existingObject.Category.ToString(),
                Public = existingObject.Public,
            };
        }
    }
}
