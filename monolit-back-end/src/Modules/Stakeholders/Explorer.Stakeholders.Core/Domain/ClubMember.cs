using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Stakeholders.Core.Domain
{
    public class ClubMember : Entity
    {
        public int TouristId { get; init; }
        public int ClubId { get; init; }


        public ClubMember() { }
        public ClubMember(int touristId, int clubId)

        {

            if (touristId == 0) throw new ArgumentException("Invalid TouristId");
            TouristId = touristId;
            if (clubId == 0) throw new ArgumentException("Invalid ClubId");
            ClubId = clubId;


        }
    }
}


