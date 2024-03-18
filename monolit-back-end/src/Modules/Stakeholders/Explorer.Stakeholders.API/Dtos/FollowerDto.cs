using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Dtos
{
    public class FollowerDto
    {
        public long UserId { get; set; }
        public string Username { get; set; }
        public DateTime Date { get; set; }
    }
}
