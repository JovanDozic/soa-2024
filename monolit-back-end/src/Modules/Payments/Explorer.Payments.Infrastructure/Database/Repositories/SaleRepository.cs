using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using Explorer.Payments.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Tours.API.Dtos.Tours;
using Microsoft.EntityFrameworkCore;
using Explorer.Payments.API.Dtos;
using Newtonsoft.Json;

namespace Explorer.Payments.Infrastructure.Database.Repositories
{
    public class SaleRepository : CrudDatabaseRepository<Sale, PaymentsContext>, ISaleRepository
    {
        private readonly PaymentsContext _context;
        private readonly DbSet<Sale> _dbSet;
        public SaleRepository(PaymentsContext dbContext) : base(dbContext)
        {
            _context = dbContext;
            _dbSet = DbContext.Set<Sale>();
        }

        public List<TourDto> GetAllToursOnSale()
        {
            var toursOnSales = new List<TourDto>();
            foreach (var sale in _dbSet) 
            {
                var saleD = JsonConvert.DeserializeObject<Sale>(sale.JsonObject);
                var toursOnSale = saleD.ToursOnSale;
                toursOnSales.AddRange(toursOnSale);
            }

            return toursOnSales;
        }
    }
}
