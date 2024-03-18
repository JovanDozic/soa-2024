using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public class PublicRegistrationRequestDto
    {
        public long Id { get; set; }
        public int ObjectId { get; set; }
        public string ObjectName { get; set; }
        public int TourId { get; set; }
        public string PointName { get; set; }
        public string Comment { get; set; }
        public string Status { get; set; }

    }
}

