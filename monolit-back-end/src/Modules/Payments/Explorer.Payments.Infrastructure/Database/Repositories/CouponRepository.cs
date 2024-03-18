using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Payments.Core.Domain;
using Explorer.Payments.Core.Domain.Order;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Infrastructure.Database.Repositories
{
    public class CouponRepository : CrudDatabaseRepository<Coupon, PaymentsContext>, ICouponRepository
    {
        private readonly PaymentsContext _context;
        private readonly DbSet<Coupon> _dbSet;
        public CouponRepository(PaymentsContext dbContext) : base(dbContext)
        {
            _context = dbContext;
            _dbSet = DbContext.Set<Coupon>();
        }
    }
}
