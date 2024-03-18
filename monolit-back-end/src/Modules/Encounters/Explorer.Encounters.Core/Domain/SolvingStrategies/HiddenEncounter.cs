using Explorer.Encounters.API.Dtos;
using Explorer.Encounters.Core.Domain.Participants;
using Explorer.Encounters.Core.Domain.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.Core.Domain.SolvingStrategies
{
    public class HiddenEncounter : Encounter
    {
        public string Image { get; private set; }
        public Location PointLocation { get; private set; }
        public HiddenEncounter() { }

        public HiddenEncounter(string image, Location pointLocation)
        {
            Validate(image, pointLocation);
            Image = image;
            PointLocation = pointLocation;
        }

        public bool Solve(string username, double longitude, double latitude)
        {
            var personsLocation = new Location(longitude, latitude);
            var inRange = DistanceCalculator.CalculateDistance(personsLocation, PointLocation) * 1000 <= 5;
            if (inRange)
            {
                Completers.Add(new Completer(username, DateTime.Now));
                Participants.Remove(new Participant(username));
            }
            return inRange;
        }

        private static void Validate(string image, Location pointLocation)
        {
            if (string.IsNullOrEmpty(image)) throw new ArgumentException("Exception! Invalid image!");
            if (pointLocation == null) throw new ArgumentNullException("Exception! Point location must not be null!");
            ;
        }
    }
}
