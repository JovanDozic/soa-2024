using AutoMapper;
using Explorer.Tours.API.Dtos.TourExecutions;
using Explorer.Tours.Core.Converters;
using Explorer.Tours.Core.Domain.TourExecutions;

namespace Explorer.Tours.Core.Mappers
{
    public class TourExecutionProfile: Profile
    {
        public TourExecutionProfile()
        {
            CreateMap<TourExecutionDto, TourExecution>()
                .ForMember(dest => dest.Position, opt =>
                    opt.MapFrom(src =>
                        new Position(
                            src.Position.Latitude,
                            src.Position.Longitude,
                            src.Position.LastActivity)))
                .ForMember(dest => dest.Tasks, opt =>
                    opt.MapFrom(src => 
                        src.Tasks.Select(x =>
                            new PointTask(PointConverter.ToDomain(x.Point), x.TaskType, x.DoneOn, x.Done))));

            CreateMap<TourExecution, TourExecutionDto>()
                .ForMember(dest => dest.Position, opt =>
                    opt.MapFrom(src =>
                        new PositionDto
                        {
                            Latitude = src.Position.Latitude,
                            Longitude = src.Position.Longitude,
                            LastActivity = src.Position.LastActivity

                        }))
                .ForMember(dest => dest.Tasks, opt =>
                    opt.MapFrom(src =>
                        src.Tasks.Select(x =>
                            new PointTaskDto
                            {
                                Done = x.Done,
                                DoneOn = x.DoneOn,
                                TaskType = x.Type,
                                Point = PointConverter.ToDto(x.Point)
                            })));
        }
    }
}
