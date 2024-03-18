using System.Text.Json.Serialization;
using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Tours.Core.Domain.Tours;
using static Explorer.Tours.API.Enums.TourEnums;

namespace Explorer.Tours.Core.Domain.TourExecutions
{
    public class PointTask: ValueObject
    {
        public bool Done { get; set; }
        public DateTime DoneOn { get; set; }
        public TaskType Type { get; set; }
        public Point Point { get; init; }

        public PointTask() {}

        [JsonConstructor]
        public PointTask(Point point, TaskType type, DateTime doneOn, bool done)
        {
            Point = point;
            Type = type;
            Done = done;
            DoneOn = doneOn;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Done;
            yield return DoneOn;
            yield return Type;
            yield return Point;
        }
    }
}
