using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.API.Dtos
{
    public class TourPurchaseTokenDto
    {
        public long TokenId { get; set; }
        public int UserId { get; set; }
        public int TourId { get; set; }
        public DateTime PurchaseTime { get; set; }
        public string TourName { get; set; }
        public string TourImage { get; set; }
    }
}
