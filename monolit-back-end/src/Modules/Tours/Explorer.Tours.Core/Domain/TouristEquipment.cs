using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain
{
    public class TouristEquipment : Entity
    {
        public int TouristId { get; init; }
        public int EquipmentId { get; init; }

        public TouristEquipment(int touristId, int equipmentId)
        {
            TouristId = touristId;
            EquipmentId = equipmentId;
            Validate();
        }

        private void Validate()
        {
            if (TouristId == 0) throw new ArgumentException("Invalid TouristId");
            if (EquipmentId == 0) throw new ArgumentException("Invalid EquipmentId");
        }
    }
}
