using Explorer.Encounters.API.Dtos;
using Explorer.Encounters.API.Public;
using Explorer.Encounters.Core.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Encounter
{
    [Route("api/hidden-encounters")]
    public class HiddenEncounterController : BaseApiController
    {
        private readonly IHiddenEncounterService _encounterService;

        public HiddenEncounterController(IHiddenEncounterService service)
        {
            _encounterService = service;
        }

        [HttpPost]
        public ActionResult<HiddenEncounterDto> Create([FromBody] HiddenEncounterDto encounter)
        {
            var result = _encounterService.Create(encounter);
            return CreateResponse(result);
        }

        [HttpPut]
        public ActionResult<HiddenEncounterDto> Update([FromBody] HiddenEncounterDto encounter)
        {
            var result = _encounterService.Update(encounter);
            return CreateResponse(result);
        }

        [HttpGet("get/{encounterId:int}")]
        public ActionResult<HiddenEncounterDto> Get([FromRoute] int encounterId)
        {
            return CreateResponse(_encounterService.Get(encounterId));
        }

        [AllowAnonymous]
        [HttpGet("getAll")]
        public ActionResult<HiddenEncounterDto> GetAll()
        {
            return CreateResponse(_encounterService.GetAll());
        }

        [Authorize(Policy = "touristPolicy")]
        [HttpPut("solve-hidden/{encounterId:int}")]
        public ActionResult<HiddenEncounterDto> SolveHidden([FromRoute] int encounterId, [FromBody] ParticipantLocationDto locationDto)
        {
            return CreateResponse(_encounterService.Solve(encounterId, locationDto));
        }
    }
}
