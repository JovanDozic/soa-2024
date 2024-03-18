using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Admin
{
    [Authorize(Policy = "administratorPolicy")]
    [Route("api/administration/users")]
    public class AdminUserController : BaseApiController
    {
        private readonly IUserService _userService;

        public AdminUserController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpGet("getAll")]
        public ActionResult<UserDto> GetAll()
        {
            return CreateResponse(_userService.GetAll());
        }

        [HttpPost("block-user")]
        public ActionResult<UserDto> BlockUsers([FromQuery] string username)
        {
            return CreateResponse(_userService.Block(username));
        }
    }
}
