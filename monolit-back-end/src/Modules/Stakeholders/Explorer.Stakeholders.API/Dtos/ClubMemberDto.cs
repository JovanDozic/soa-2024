using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Dtos
{
    public class ClubMemberDto
    {
        public int Id { get; set; }
        public int TouristId { get; set; }
        public int ClubId { get; set; }
    }
}
