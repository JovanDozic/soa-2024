using Explorer.Tours.API.Dtos.Tours;
using Explorer.Tours.Core.Domain.Tours;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.API.Dtos
{
    public class SaleDto
    {
        public long Id { get; set; }
        public List<TourDto>? ToursOnSale { get; set; }
        public DateTime SaleStart { get; set; }
        public DateTime SaleEnd { get; set; }
        public int DiscountPercentage { get; set; }
        public bool IsActive { get; set; }
    }
}
