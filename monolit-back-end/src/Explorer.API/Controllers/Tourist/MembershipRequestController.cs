using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.UseCases;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json;

namespace Explorer.API.Controllers.Tourist
{
    //[Authorize(Policy = "_ROLE_Policy")]
    [Route("api/membershipRequests")]
    public class MembershipRequestController : BaseApiController
    {
        private readonly IMembershipRequestService _membershipRequestService;

        public MembershipRequestController(IMembershipRequestService membershipRequestService)
        {
            _membershipRequestService = membershipRequestService;

        }

        [HttpPut("accept/{id:int}")]
        public ActionResult<MembershipRequestDto> Accept(int id)
        {
            var result = _membershipRequestService.AcceptMembershipRequest(id);
            return CreateResponse(result);
        }

        [HttpGet("getAll")]
        public ActionResult<PagedResult<MembershipRequestDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            return CreateResponse(_membershipRequestService.GetPaged(page, pageSize));
        }
        [HttpPost]
        public ActionResult<MembershipRequestDto> Create([FromBody] MembershipRequestDto req)
        {
            if (HttpContext.User.Identity != null)
            {
                var userId = int.Parse(HttpContext.User.Claims.First(c => c.Type == "id").Value);
                req.TouristId = userId;
                var result = _membershipRequestService.CreateMembershipRequest(req);
                return CreateResponse(result);
            }

            return Unauthorized();
        }  
      
        [HttpPut("reject/{id:int}")]
        public ActionResult<MembershipRequestDto> Reject(int id)
        {
            var result = _membershipRequestService.RejectMembershipRequest(id);
            return CreateResponse(result);
        }
        

    }
}
