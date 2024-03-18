using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/tourist/position")]
    public class TouristPositionController : BaseApiController
    {
        private readonly ITouristPositionService _positionService;

        public TouristPositionController(ITouristPositionService touristPositionService)
        {
            _positionService = touristPositionService;
        }

        //TODO This will be patch method when aggregate is implemented

        [HttpPut("{id:int}")]
        public ActionResult<TouristPositionDto> Update([FromBody]TouristPositionDto dto) 
        { 
            var result = _positionService.Update(dto);
            return CreateResponse(result);
        }
    }
}
