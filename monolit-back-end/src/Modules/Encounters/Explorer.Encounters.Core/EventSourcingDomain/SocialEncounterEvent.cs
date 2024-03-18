using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Encounters.API.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Explorer.Encounters.Core.EventSourcingDomain
{
    public class SocialEncounterEvent : ValueObject
    {
        public DateTime EncounterSolved { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Username { get; set; }
        public SocialEventType Type { get; set; }

        [Newtonsoft.Json.JsonConstructor]
        public SocialEncounterEvent(DateTime encounterSolved, string username, double longitude, double latitude, SocialEventType type)
        {
            EncounterSolved = encounterSolved;
            Username = username;
            Longitude = longitude;
            Latitude = latitude;
            Type = type;
        }
        public SocialEncounterEvent()  { }


        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return EncounterSolved;
            yield return Latitude; 
            yield return Longitude;
            yield return Username;
            yield return Type;
        }
    }
}
