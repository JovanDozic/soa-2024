using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Administrator.Administration
{
    [Route("api/administration/app-ratings")]
    public class AppRatingController : BaseApiController
    {
        private readonly IAppRatingService _appRatingService;

        public AppRatingController(IAppRatingService appRatingService)
        {
            _appRatingService = appRatingService;
        }

        [HttpGet]
        [Authorize(Policy = "administratorPolicy")]
        public ActionResult<PagedResult<AppRatingDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _appRatingService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

        [HttpPost]
        [Authorize(Policy = "touristAndAuthorPolicy")]
        public ActionResult<AppRatingDto> Create([FromBody] AppRatingDto appRating)
        {
            bool userAlreadyRated = _appRatingService.HasUserRated(appRating.UserId);
            if (userAlreadyRated) { return BadRequest("User has already rated the app."); }

            var result = _appRatingService.Create(appRating);
            return CreateResponse(result);
        }
    }

}