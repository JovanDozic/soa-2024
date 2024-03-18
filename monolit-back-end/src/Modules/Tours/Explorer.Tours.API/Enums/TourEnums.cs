namespace Explorer.Tours.API.Enums
{
    public class TourEnums
    {
        public enum TransportType
        {
            Walk,
            Car,
            Bicycle
        }

        public enum TourStatus 
        {
            Draft,
            Published,
            Archived
        }

        public enum TourExecutionStatus 
        { 
            Active = 1,
            Completed,
            Abandoned
        }

        public enum TaskType
        {
            Point = 1
        }
    }
}
