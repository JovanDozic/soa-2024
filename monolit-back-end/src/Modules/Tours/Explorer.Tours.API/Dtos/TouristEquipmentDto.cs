﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public class TouristEquipmentDto
    {
        public int Id { get; set; } = 0;
        public int TouristId { get; set; }
        public int EquipmentId { get; set; }
    }
}
