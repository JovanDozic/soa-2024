using Explorer.Tours.API.Dtos.Tours;
using static Explorer.Tours.API.Enums.TourEnums;

namespace Explorer.Tours.API.Dtos.TourExecutions
{
    public class PointTaskDto
    {
        public bool Done { get; set; }
        public DateTime DoneOn { get; set; }
        public PointDto Point { get; set; }
        public TaskType TaskType { get; set; }
    }
}
