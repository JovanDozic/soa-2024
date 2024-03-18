using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public.Shopping;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Shopping
{
    //[Authorize(Policy = "touristPolicy")]
    [Route("api/tourist/order")]
    public class OrderController : BaseApiController
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public ActionResult<PagedResult<ShoppingCartDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _orderService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

        [HttpGet("{id:int}")]
        public ActionResult<ShoppingCartDto> GetByIdUser(int id)
        {
            var result = _orderService.GetByIdUser(id);
            return CreateResponse(result);
        }

        [HttpPost]
        public ActionResult<ShoppingCartDto> Create([FromBody] ShoppingCartDto cart)
        {
            var result = _orderService.Create(cart);
            return CreateResponse(result);
        }

        [HttpPost("addToCart")]
        public ActionResult<ShoppingCartDto> AddToCart([FromBody] OrderItemDto orderItem, [FromQuery] int userId)
        {
            var result = _orderService.AddToCart(orderItem, userId);
            return CreateResponse(result);
        }

        [HttpPut]
        public ActionResult<ShoppingCartDto> Update([FromBody] ShoppingCartDto cart)
        {
            var result = _orderService.Update(cart);
            return CreateResponse(result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _orderService.Delete(id);
            return CreateResponse(result);
        }
    }
}
