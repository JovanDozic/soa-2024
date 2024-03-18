using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Dtos.Tours;
using Explorer.Tours.API.Public;
using Explorer.Tours.API.Public.Administration;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Administrator.Administration
{
    [Route("api/administration/problems")]
    [Authorize(Policy = "administratorPolicy")]
    public class ProblemController : BaseApiController
    {
        private readonly IProblemService _problemService;
        public ProblemController(IProblemService problemService)
        {
            _problemService = problemService;
        }
        /*
        [HttpGet]
        public ActionResult<PagedResult<ProblemDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _problemService.GetPaged(page, pageSize);

            return CreateResponse(result);
        }

        [HttpGet("unresolved-with-deadline")]
        public ActionResult<Result<List<ProblemDto>>> GetUnresolvedProblemsWithDeadline()
        {
            var allProblems = _problemService.GetAll(); // Fetch all problems (if needed)


            if (allProblems.IsSuccess)
            {
                return Ok(allProblems.Value);
            }
            else
            {
                // Handle error, return a specific error response
                return BadRequest("Failed to retrieve unresolved problems with expired deadlines.");
            }
        }
        [HttpPatch("set-deadline/{id}")]
        public ActionResult SetProblemDeadline(long id, [FromBody] DateTime newDeadline)
        {
            var result = _problemService.SetProblemDeadline(id, newDeadline);



            return Ok(result);
        }
        [HttpDelete("{id}/delete")]
        public IActionResult DeleteProblemOrTour(long id)
        {
            var result = _problemService.DeleteProblem(id);

            if (result.IsSuccess)
            {
                return Ok("Problem or Tour successfully deleted.");
            }

            return BadRequest("Failed to delete Problem or Tour.");
        }*/
    }
}