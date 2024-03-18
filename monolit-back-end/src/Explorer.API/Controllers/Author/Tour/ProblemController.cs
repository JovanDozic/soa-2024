using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain.Users;
using Explorer.Stakeholders.Core.UseCases;
using Explorer.Tours.API.Dtos.Tours;
using Explorer.Tours.API.Public.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Author.Tour
{
    [Route("api/author/problems")]
    //[Authorize(Policy = "authorPolicy")]
    public class ProblemController : BaseApiController
    {
        private readonly IProblemService _problemService;
        private readonly IUserNotificationService _userNotificationService;
        private readonly ITourService _tourService;
        public ProblemController(IProblemService problemService, IUserNotificationService userNotificationService, ITourService tourService)
        {
            _problemService = problemService;
            _userNotificationService = userNotificationService;
            _tourService = tourService;
        }
        [HttpGet("getAll/{authorsId:int}")]
        public ActionResult<PagedResult<ProblemDto>> GetAll(int authorsId)
        {
            var result = _tourService.GetAllProblems(authorsId);
            return CreateResponse(result);
        }

        [HttpPatch("respondToProblem/{id:int}/{response}")]
        public ActionResult<PagedResult<ProblemDto>> RespondToProblem(int id, string response)
        {
            var result = _problemService.RespondToProblem(id, response);
            var problem = result.Value;
            return CreateResponse(result);
        }
        
        [HttpGet("getToursProblems/{id:int}")]
        public ActionResult<PagedResult<ProblemDto>> GetToursProblems(int id)
        {
            var result = _problemService.GetToursProblems(id);

            return CreateResponse(result);
        }
    }
}

