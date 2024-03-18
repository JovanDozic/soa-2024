using static Explorer.Tours.API.Enums.TourEnums;


namespace Explorer.Tours.API.Dtos.TourExecutions
{
    public class TourExecutionDto
    {
        public int Id { get; set; }
        public TourExecutionStatus Status { get; set; }
        public PositionDto? Position { get; set; }
        public List<PointTaskDto>? Tasks { get; set; }
        public int TourId { get; set; }
    }
}
