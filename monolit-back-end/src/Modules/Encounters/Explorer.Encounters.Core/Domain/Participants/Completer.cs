using Explorer.BuildingBlocks.Core.Domain;
using Newtonsoft.Json;

namespace Explorer.Encounters.Core.Domain.Participants
{
    public class Completer : ValueObject
    {
        public string Username { get; init; }
        public DateTime CompletionDate { get; init; }


        [JsonConstructor]
        public Completer(string username, DateTime completionDate)
        {
            Validate(username, completionDate);
            Username = username;
            CompletionDate = completionDate;
        }

        private void Validate(string username, DateTime completionDate)
        {
            if (string.IsNullOrEmpty(username)) throw new ArgumentNullException("Exception! Username should not be empty");
            if (completionDate.Equals(DateTime.MinValue) || completionDate.Equals(DateTime.MaxValue))
                throw new ArgumentNullException("Exception! Invalid date");
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Username;
            yield return CompletionDate;
        }
    }
}
