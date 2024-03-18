using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Personalization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist.Personalization
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/personalization/preference")]
    public class PreferenceController : BaseApiController
    {
        private readonly IPreferenceService _preferenceService;

        public PreferenceController(IPreferenceService preferenceService)
        {
            _preferenceService = preferenceService;
        }

        [HttpGet]
        public ActionResult<PagedResult<PreferenceDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _preferenceService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

        [HttpGet("getAllForTourist/{id}")]
        public ActionResult<List<PreferenceDto>> GetAllForForTourist(int id)
        {
            var result = _preferenceService.GetAllForTourist(id);
            return CreateResponse(result);
        }

        [HttpPost]
        public ActionResult<PreferenceDto> Create([FromBody] PreferenceDto preference)
        {
            var result = _preferenceService.Create(preference);
            return CreateResponse(result);
        }

        [HttpPut("{id:int}")]
        public ActionResult<PreferenceDto> Update([FromBody] PreferenceDto preference)
        {
            var result = _preferenceService.Update(preference);
            return CreateResponse(result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _preferenceService.Delete(id);
            return CreateResponse(result);
        }
    }
}
