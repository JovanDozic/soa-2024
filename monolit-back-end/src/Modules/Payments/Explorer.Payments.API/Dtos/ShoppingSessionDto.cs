using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.API.Dtos
{
    public class ShoppingSessionDto
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public List<ShoppingEventDto> Events { get; set; }
        public bool IsActive { get; set; }
    }
}
