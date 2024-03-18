using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Explorer.Payments.Core.Domain.Session;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist.Sessions
{
    //[Authorize(Policy = "touristPolicy")]
    [Route("api/tourist/shoppingSession")]
    public class ShoppingSessionController : BaseApiController
    {
        private readonly IShoppingSessionService _service;

        public ShoppingSessionController(IShoppingSessionService service)
        {
            _service = service;
        }

        [HttpPost("startSession/{userId}")]
        public ActionResult<ShoppingSession> StartSession(long userId)
        {
            return CreateResponse(_service.StartSession(userId));
        }

        [HttpGet("closeSession/{userId}")]
        public ActionResult<ShoppingSession> CloseSession(long userId)
        {
            return CreateResponse(_service.CloseSession(userId));
        }

        [HttpPatch("addEvent/{userId}")]
        public ActionResult<ShoppingSession> AddEvent([FromBody] ShoppingEventDto newEvent, long userId)
        {
            return CreateResponse(_service.AddEvent(newEvent, userId));
        }
    }
}
