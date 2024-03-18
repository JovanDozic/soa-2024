using Explorer.Encounters.API.Dtos;
using Explorer.Encounters.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Encounter
{
    [Route("api/misc-encounters")]
    public class MiscEncounterController: BaseApiController
    {
        private readonly IMiscEncounterService _encounterService;

        public MiscEncounterController(IMiscEncounterService service)
        {
            _encounterService = service;
        }
        [HttpPost]
        public ActionResult<MiscEncounterDto> Create([FromBody] MiscEncounterDto encounter)
        {
            var result = _encounterService.Create(encounter);
            return CreateResponse(result);
        }

        [HttpPut]
        public ActionResult<MiscEncounterDto> Update([FromBody] MiscEncounterDto encounter)
        {
            var result = _encounterService.Update(encounter);
            return CreateResponse(result);
        }

        [HttpGet("get/{encounterId:int}")]
        public ActionResult<MiscEncounterDto> Get([FromRoute] int encounterId)
        {
            return CreateResponse(_encounterService.Get(encounterId));
        }

        [AllowAnonymous]
        [HttpGet("getAll")]
        public ActionResult<MiscEncounterDto> GetAll()
        {
            return CreateResponse(_encounterService.GetAll());
        }

        [Authorize(Policy = "touristPolicy")]
        [HttpPut("solve-misc/{encounterId:int}")]
        public ActionResult<MiscEncounterDto> SolveSocial([FromRoute] int encounterId, [FromQuery] string username)
        {
            return CreateResponse(_encounterService.Solve(encounterId, username));
        }
    }
}
