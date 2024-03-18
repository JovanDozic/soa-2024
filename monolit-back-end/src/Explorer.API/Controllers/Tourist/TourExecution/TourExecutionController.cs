using Explorer.Tours.API.Dtos.TourExecutions;
using Explorer.Tours.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Explorer.API.Controllers.Tourist.TourExecution
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/tourist/tourExecution")]
    public class TourExecutionController: BaseApiController
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

        [HttpPost("start-execution/{tourId:int}")]
        public ActionResult<TourExecutionDto> StartExecution([FromRoute] int tourId)
        {
            return CreateResponse(_service.StartExecution(tourId));
        }

        [HttpPatch("quit/{tourExecutionId:int}")]
        public ActionResult<TourExecutionDto> QuitExecution([FromRoute] int tourExecutionId)
        {
            return CreateResponse(_service.QuitExecution(tourExecutionId));
        }

        [HttpPut("update-position/{tourExecutionId:int}")]
        public ActionResult<TourExecutionDto> UpdatePosition([FromBody] PositionDto positionDto,
            [FromRoute] int tourExecutionId)
        {
            return CreateResponse(_service.UpdatePosition(tourExecutionId, positionDto));
        }

    }
}
