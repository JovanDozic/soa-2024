using Explorer.Encounters.Core.Domain.Participants;

namespace Explorer.Encounters.Core.Domain.SolvingStrategies
{
    public class MiscEncounter: Encounter
    {
        public MiscEncounter()
        {
            
        }

        public List<Completer> Solve(string username)
        {
            if (Participants.Any(u => u.Username.Equals(username)))
            {
                var participant = Participants.FirstOrDefault(u => u.Username.Equals(username));
                List<Completer> completers = new List<Completer>
                {
                    new Completer(username, DateTime.Now)
                };
                Completers.AddRange(completers);
                Participants.Remove(participant ?? throw new NullReferenceException());
                return completers;
            }
            return new List<Completer>();
        }
    }
}