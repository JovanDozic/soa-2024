using Explorer.BuildingBlocks.Core.Domain;
using System.Text.Json.Serialization;
using static Explorer.Tours.API.Enums.TourEnums;

namespace Explorer.Tours.Core.Domain.Tours
{
    public class RequiredTime : ValueObject
    {
        public TransportType TransportType { get; private set; }
        public int Minutes { get; private set; }

        [Newtonsoft.Json.JsonConstructor]
        public RequiredTime(TransportType transportType, int minutes)
        {
            TransportType = transportType;
            Minutes = minutes;
            Validate();
        }

        private void Validate()
        {
            if (Minutes <= 0)
                throw new ArgumentOutOfRangeException("Invalid value for minutes!");
        }

        public void Update(RequiredTime dataIn)
        {
            TransportType = dataIn.TransportType;
            Minutes = dataIn.Minutes;
            Validate();
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return TransportType;
            yield return Minutes;
        }
    }
}
