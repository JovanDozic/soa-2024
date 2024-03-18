using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain
{
    public class EquipmentManagment : Entity
    {
        public int AuthorId { get; init; }
        public int EquipmentId { get; init; }
        public int TourId { get; init; }

        public EquipmentManagment(int authorId, int equipmentId, int tourId)
        {
            AuthorId = authorId;
            EquipmentId = equipmentId;
            TourId = tourId;
            Validate();
        }

        private void Validate()
        {
            if (AuthorId == 0) throw new ArgumentException("Invalid author ID");
            if (EquipmentId == 0) throw new ArgumentException("Invalid equipment ID");
            if (TourId == 0) throw new ArgumentException("Invalid tour ID");
        }
    }
}
