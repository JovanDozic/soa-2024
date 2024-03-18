using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Payments.Core.Domain.Order;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using Explorer.Payments.Core.Domain.Session;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Infrastructure.Database.Repositories
{
    public class ShoppingSessionRepository : CrudDatabaseRepository<ShoppingSession, PaymentsContext>, IShoppingSessionRepository
    {
        private readonly PaymentsContext _context;
        private readonly DbSet<ShoppingSession> _dbSet;
        public ShoppingSessionRepository(PaymentsContext dbContext) : base(dbContext)
        {
            _context = dbContext;
            _dbSet = DbContext.Set<ShoppingSession>();
        }

        public ShoppingSession GetActivetByUserId(long userId)
        {
            return _dbSet.FirstOrDefault(x => x.UserId == userId && x.IsActive);
        }
    }
}
