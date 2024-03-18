using Explorer.Encounters.API.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.API.Dtos
{
    public class SocialEncounterEventDto
    {
        public DateTime EncounterSolved { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Username { get; set; }
        public SocialEventType Type { get; set; }
    }
}
