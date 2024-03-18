using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Dtos
{
    public class MembershipRequestDto
    {
        public int Id { get; init; }
        public int TouristId { get; set; }
        public int ClubId { get; init; }
        public bool Accepted { get; init; }
    }
}
