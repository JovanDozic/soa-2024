using Explorer.BuildingBlocks.Core.Domain;
using Newtonsoft.Json;

namespace Explorer.Encounters.Core.Domain.Participants
{
    public class Participant : ValueObject
    {
        public string Username { get; init; }

        public Participant() {}

        [JsonConstructor]
        public Participant(string username)
        {
            Validate(username);
            Username = username;
        }

        private static void Validate(string username)
        {
            if (string.IsNullOrEmpty(username)) throw new ArgumentNullException("Exception! Username should not be empty");
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Username;
        }
    }
}
