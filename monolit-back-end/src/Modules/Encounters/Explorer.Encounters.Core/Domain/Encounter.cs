using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Encounters.API.Enums;
using Explorer.Encounters.Core.Domain.Participants;
using Explorer.Encounters.Core.Domain.Utilities;
using System.Diagnostics.CodeAnalysis;

namespace Explorer.Encounters.Core.Domain
{
    public class Encounter: Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Location Location { get; set; }
        public int Experience { get; set; }
        public EncounterStatus Status { get; set; }
        public EncounterType Type { get; init; }
        public int Radius { get; init; }
        public List<Participant> Participants { get; set; } = new List<Participant>();
        public List<Completer> Completers { get; set; } = new List<Completer>();
        public Encounter() {}

        public Encounter(string name, string description, Location location, int experience, EncounterStatus status, EncounterType type, int radius)
        {
            Name = name;
            Description = description;
            Location = location;
            Status = status;
            Type = type;
            Experience = experience;
            Radius = radius;
        }

        public bool Activate(string username, double longitude, double latitude)
        {
            if (Participants.Any(x => x.Username.Equals(username))) return false; //Already activated
            if (Completers.Any(x => x.Username.Equals(username))) return false; //Already completed
            var personsLocation = new Location(longitude, latitude);
            var inProximity =  DistanceCalculator.CalculateDistance(personsLocation, Location) * 1000 <= Radius;
            if (inProximity) Participants.Add(new Participant(username));
            return inProximity; //Activation result, positive/negative
        }

    }
}
