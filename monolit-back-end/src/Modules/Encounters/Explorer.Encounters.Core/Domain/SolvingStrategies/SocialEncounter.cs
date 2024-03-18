using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Encounters.API.Enums;
using Explorer.Encounters.Core.Domain.Participants;
using Explorer.Encounters.Core.Domain.Utilities;
using Explorer.Encounters.Core.EventSourcingDomain;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations.Schema;

namespace Explorer.Encounters.Core.Domain.SolvingStrategies
{
    public class SocialEncounter: Encounter
    {
        public int RequiredParticipants { get; private set; }
        public List<Participant> CurrentlyInRange { get; private set; } = new List<Participant>();
        public List<SocialEncounterEvent> Events { get; private set; }
        [NotMapped]
        public List<Completer> SolveResult { get; set; }
        [NotMapped]
        public bool ActivateResult { get; set; }

        public SocialEncounter(){}

        public SocialEncounter(int requiredParticipants, List<Participant> currentlyInRange, List<SocialEncounterEvent> socialChanges)
        {
            Validate(requiredParticipants, currentlyInRange);
            RequiredParticipants = requiredParticipants;
            CurrentlyInRange = currentlyInRange;
        }

        public bool ActivateSocialEncounter(string username, double longitude, double latitude)
        {
            Causes(new SocialEncounterEvent(DateTime.Now, username, longitude, latitude, SocialEventType.Activate));
            return ActivateResult;
        }

        public List<Completer> Solve(string username, double longitude, double latitude)
        {
            Causes(new SocialEncounterEvent(DateTime.Now, username, longitude, latitude, SocialEventType.Solved));
            return SolveResult;
        }

        private static void Validate(int requiredParticipants, List<Participant> currentlyInRange)
        {
            if (requiredParticipants < 1) throw new ArgumentException("Exception! Must be above 0");
            if (currentlyInRange == null) throw new ArgumentNullException("Exception! Must not be null!");
        }

        private void Causes(SocialEncounterEvent @event)
        {
            Events.Add(@event);
            Apply(@event);
        }

        public void Apply(SocialEncounterEvent @event)
        {
            switch (@event.Type)
            {
                case SocialEventType.Solved:
                    WhenSolve(@event);
                    break;

                case SocialEventType.Activate:
                    WhenActivate(@event);
                    break;

                // Add more cases as needed...

                default:
                    // Handle default case or throw an exception if necessary
                    break;
            }
        }

        private List<Completer> WhenSolve(SocialEncounterEvent encounterSolved)
        {
            var participantsLocation = new Location(encounterSolved.Longitude, encounterSolved.Latitude);
            var inProximity = DistanceCalculator.CalculateDistance(participantsLocation, Location) * 1000 <= Radius;
            if (!inProximity && CurrentlyInRange.Any(x => x.Username.Equals(encounterSolved.Username)))
                CurrentlyInRange.Remove(CurrentlyInRange.Find(x => x.Username.Equals(encounterSolved.Username)) ?? throw new NullReferenceException("Exception!"));
            if (inProximity && CurrentlyInRange.All(y => !y.Username.Equals(encounterSolved.Username))) CurrentlyInRange.Add(new Participant(encounterSolved.Username));
            var isSolved = RequiredParticipants == CurrentlyInRange.Count;
            if (isSolved)
            {
                List<Completer> completers = Participants.Select(participant => new Completer(participant.Username, DateTime.Now)).ToList();
                Completers.AddRange(completers);
                Participants.Clear();
                CurrentlyInRange.Clear();
                SolveResult = completers;
            }
            SolveResult =  new List<Completer>();
            return SolveResult;
        }

        public bool WhenActivate(SocialEncounterEvent encounterActivated)
        {
            if (Participants.Any(x => x.Username.Equals(encounterActivated.Username))) return false; //Already activated
            if (Completers.Any(x => x.Username.Equals(encounterActivated.Username))) return false; //Already completed
            var personsLocation = new Location(encounterActivated.Longitude, encounterActivated.Latitude);
            var inProximity = DistanceCalculator.CalculateDistance(personsLocation, Location) * 1000 <= Radius;
            if (inProximity) Participants.Add(new Participant(encounterActivated.Username));
            ActivateResult = inProximity; //Activation result, positive/negative
            return ActivateResult;
        }
    }
}
