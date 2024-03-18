using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;

namespace Explorer.API.Controllers.Tourist
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/tourist/clubInvitation")]
    public class ClubInvitationController : BaseApiController
    {
        private readonly IClubInvitationService _clubInvitationService;

        public ClubInvitationController(IClubInvitationService clubInvitationService)
        {
            _clubInvitationService = clubInvitationService;

        }




        [HttpGet("getAll")]
        public ActionResult<PagedResult<ClubInvitationDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            return CreateResponse(_clubInvitationService.GetPaged(page, pageSize));
        }



        [HttpPost]
        public ActionResult<ClubInvitationDto> Create([FromBody] ClubInvitationDto clubInvitation)
        {
            

            if (HttpContext.User.Identity != null)
            {
                var userId = int.Parse(HttpContext.User.Claims.First(c => c.Type == "id").Value);
               
              
                if (!_clubInvitationService.IsInvitationOwner(userId,clubInvitation.ClubId))
                {
                    return BadRequest("You are not owner of the club.");
                }
            }

                var result = _clubInvitationService.Create(clubInvitation);
            return CreateResponse(result);
        }


        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _clubInvitationService.Delete(id);
            return CreateResponse(result);
        }

    }
}
