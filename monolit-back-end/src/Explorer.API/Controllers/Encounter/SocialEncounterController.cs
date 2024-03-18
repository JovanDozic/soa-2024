using Explorer.Encounters.API.Dtos;
using Explorer.Encounters.API.Public;
using Explorer.Encounters.Core.Domain.RepositoryInterfaces;
using Explorer.Encounters.Core.UseCasesEvent;
using Explorer.Encounters.Infrastructure.Database.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Encounter
{
    [Route("api/social-encounters")]
    public class SocialEncounterController: BaseApiController
    {
            private readonly ISocialEncounterService _encounterService;

            public SocialEncounterController(ISocialEncounterService service)
            {
                _encounterService = service;
            }
            
            [HttpPost]
            public ActionResult<SocialEncounterDto> Create([FromBody] SocialEncounterDto encounter)
            {
                var result = _encounterService.Create(encounter);
                return CreateResponse(result);
            }

            [HttpPut]
            public ActionResult<SocialEncounterDto> Update([FromBody] SocialEncounterDto encounter)
            {
                var result = _encounterService.Update(encounter);
                return CreateResponse(result);
            }

            [HttpGet("get/{encounterId:int}")]
            public ActionResult<SocialEncounterDto> Get([FromRoute] int encounterId)
            {
                return CreateResponse(_encounterService.Get(encounterId));
            }

            [AllowAnonymous]
            [HttpGet("getAll")]
            public ActionResult<SocialEncounterDto> GetAll()
            {
                return CreateResponse(_encounterService.GetAll());
            }

            [Authorize(Policy = "touristPolicy")]
            [HttpPut("activate-social/{encounterId:int}")]
            public ActionResult<SocialEncounterDto> ActivateSocial([FromRoute] int encounterId, [FromBody] ParticipantLocationDto locationDto)
            {
                return CreateResponse(_encounterService.Activate(encounterId, locationDto));
            }

        [Authorize(Policy = "touristPolicy")]
            [HttpPut("solve-social/{encounterId:int}")]
            public ActionResult<SocialEncounterDto> SolveSocial([FromRoute] int encounterId, [FromBody] ParticipantLocationDto locationDto)
            {
                return CreateResponse(_encounterService.Solve(encounterId, locationDto));
            }
        }
}
