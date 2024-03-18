using Explorer.BuildingBlocks.Core.Domain;
using System.Text.Json.Serialization;

namespace Explorer.Tours.Core.Domain.Tours
{
    public class Tag : ValueObject
    {
        public string Name { get; }

        [Newtonsoft.Json.JsonConstructor]
        public Tag(string name)
        {
            Name = name;
            Validate();
        }

        private void Validate()
        {
            if (string.IsNullOrEmpty(Name))
                throw new ArgumentNullException("Invalid name");
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            throw new NotImplementedException();
        }
    }
}
