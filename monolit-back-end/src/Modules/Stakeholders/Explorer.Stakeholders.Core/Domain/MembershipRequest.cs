using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Stakeholders.API.Public;

namespace Explorer.Stakeholders.Core.Domain
{
    public class MembershipRequest : Entity
    {
        public int TouristId { get; init; }
        public int ClubId { get; init; }
        public bool IsAccepted { get; set; }

        public MembershipRequest(int touristId, int clubId)
        {
            TouristId = touristId;
            ClubId = clubId;
        }
    }


}
