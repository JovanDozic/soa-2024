using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist
{
    //[Authorize(Policy = "touristPolicy")]
    [Route("api/tourist/tourPurchaseToken")]
    public class TourPurchaseTokenController : BaseApiController
    {
        private readonly ITourPurchaseTokenService _tokenService;
        public TourPurchaseTokenController(ITourPurchaseTokenService service)
        {
            _tokenService = service;
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult<List<TourPurchaseTokenDto>> PurchaseItemsFromCart([FromBody] ShoppingCartDto shoppingCart)
        {
            var result = _tokenService.PurchaseItemsFromCart(shoppingCart);
            return CreateResponse(result);
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult<PagedResult<TourPurchaseTokenDto>> GetPaged([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _tokenService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }
        [AllowAnonymous]
        [HttpGet("{idUser:int}/{idTour:int}")]
        public ActionResult<bool> GetToken(int idUser, int idTour)
        {
            var result = _tokenService.GetToken(idUser, idTour);
            return CreateResponse(result);
        }

        [HttpGet("{userId:int}")]
        public ActionResult<List<TourPurchaseTokenDto>> GetAllForUser(int userId)
        {
            var result = _tokenService.GetAllForUser(userId);
            return CreateResponse(result);
        }

        [HttpGet("purchaseCount/{idUser:int}/{idTour:int}")]
        public ActionResult<int> GetToursPurchaseCount(int idTour)
        {
            var result = _tokenService.GetToursPurchaseCount(idTour);
            return CreateResponse(result);
        }

        [HttpGet("purchasedTourCount/{idUser:int}")]
        public ActionResult<int> GetAllPrchasedToursCount()
        {
            var result = _tokenService.GetAuthorsPurchasedTours();
            return CreateResponse(result);
        }
    }
}
