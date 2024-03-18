using Explorer.Tours.API.Dtos.TourExecutions;
using Explorer.Tours.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Author
{
    //[Authorize(Policy = "touristPolicy")]
    [Route("api/author/tourExecution")]
    public class TourExecutionController : BaseApiController
    {
        private readonly ITourExecutionService _service;
        public TourExecutionController(ITourExecutionService service)
        {
            _service = service;
        }

        [HttpGet("getAll")]
        public ActionResult<TourExecutionDto> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            return CreateResponse(_service.GetPaged(page, pageSize));
        }

        [HttpPut("update-position/{tourExecutionId:int}")]
        public ActionResult<TourExecutionDto> UpdatePosition([FromBody] PositionDto positionDto,
            [FromRoute] int tourExecutionId)
        {
            return CreateResponse(_service.UpdatePosition(tourExecutionId, positionDto));
        }

        [HttpGet("getActiveTourCount/{tourId:int}")]
        public ActionResult<int> getActiveTourCount([FromRoute] int tourId)
        {
            return CreateResponse(_service.getActiveTourCount(tourId));
        }

        [HttpGet("getCompletedTourCount/{tourId:int}")]
        public ActionResult<int> getCompletedTourCount([FromRoute] int tourId)
        {
            return CreateResponse(_service.getCompletedTourCount(tourId));
        }
        [HttpGet("getAllActiveToursCount")]
        public ActionResult<int> getAllActiveToursCount()
        {
            return CreateResponse(_service.getAllActiveToursCount());
        }
        [HttpGet("getAllCompletedToursCount")]
        public ActionResult<int> getAllCompletedToursCount()
        {
            return CreateResponse(_service.getAllCompletedToursCount());
        }
    }
}
