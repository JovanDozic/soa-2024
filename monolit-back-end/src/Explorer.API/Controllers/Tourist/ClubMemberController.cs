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
    [Route("api/tourist/clubMember")]
    public class ClubMemberController : BaseApiController
    {
        private readonly IClubMemberService _clubMemberService;

        public ClubMemberController(IClubMemberService clubMemberService)
        {
            _clubMemberService = clubMemberService;

        }


        [HttpGet("getAll")]
        public ActionResult<PagedResult<ClubMemberDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            return CreateResponse(_clubMemberService.GetPaged(page, pageSize));
        }



        [HttpPost]
        public ActionResult<ClubMemberDto> Create([FromBody] ClubMemberDto clubMember)
        {

            var result = _clubMemberService.Create(clubMember);
            return CreateResponse(result);
        }


        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _clubMemberService.Delete(id);
            return CreateResponse(result);
        }

    }
}

