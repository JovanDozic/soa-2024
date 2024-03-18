using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.UseCases.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Administrator.Administration
{
    [Route("api/administration/registrationRequests")]
    public class PublicRegistrationRequestController : BaseApiController
    {
        private readonly IPublicRegistrationRequestService _publicRegistrationRequestService;

        public PublicRegistrationRequestController(IPublicRegistrationRequestService publicRegistrationRequestService)
        {
            _publicRegistrationRequestService = publicRegistrationRequestService;
        }

        [HttpGet]
        [Authorize(Policy = "administratorPolicy")]
        public ActionResult<PagedResult<PublicRegistrationRequestDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _publicRegistrationRequestService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }
        [HttpGet("getAllPending")]
        [Authorize(Policy = "administratorPolicy")]
        public ActionResult<List<PublicRegistrationRequestDto>> GetAllPendingRequests()
        {
            var result = _publicRegistrationRequestService.GetAllPendingRequests();
            return CreateResponse(result);
        }

        [HttpPost]
        [Authorize(Policy = "authorPolicy")]
        public ActionResult<PublicRegistrationRequestDto> Create([FromBody] PublicRegistrationRequestDto publicRegistrationRequest)
        {
            var result = _publicRegistrationRequestService.Create(publicRegistrationRequest);
            return CreateResponse(result);
        }

        [HttpPut("{id:int}")]
        [Authorize(Policy = "administratorPolicy")]
        public ActionResult<PublicRegistrationRequestDto> Update([FromBody] PublicRegistrationRequestDto publicRegistrationRequest)
        {
            var result = _publicRegistrationRequestService.Update(publicRegistrationRequest);
            return CreateResponse(result);
        }
    }

}