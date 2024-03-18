using Explorer.BuildingBlocks.Core.Domain;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace Explorer.Tours.Core.Domain.Tours
{
    public class Guide : Entity
    {
        [NotMapped]
        [JsonProperty]
        public string Name { get; init; }
        [NotMapped]
        [JsonProperty]
        public string Surname { get; init; }
        [NotMapped]
        [JsonProperty]
        public string Email { get; init; }

        public Guide() { }

        [JsonConstructor]
        public Guide(long id, string name, string surname, string email)
        {
            Id = id;
            Name = name;
            Surname = surname;
            Email = email;
            //Validate();
        }

        private void Validate()
        {
            if (string.IsNullOrEmpty(Name))
                throw new ArgumentNullException("Invalid name");
            if (string.IsNullOrEmpty(Surname))
                throw new ArgumentNullException("Invalid surname");
            if (string.IsNullOrEmpty(Email))
                throw new ArgumentNullException("Invalid email");
        }
    }
}
