using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Explorer.Payments.API.Public.Shopping;
using Explorer.Payments.Core.UseCases.Shopping;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Author
{
    [Authorize(Policy = "authorPolicy")]
    [Route("api/author/coupons")]
    public class CouponController : BaseApiController
    {
        private readonly ICouponService _couponService;
        public CouponController(ICouponService couponService)
        {
            _couponService = couponService;
        }

        [HttpGet]
        public ActionResult<PagedResult<CouponDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _couponService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }


        [HttpPost]
        public ActionResult<CouponDto> Create([FromBody] CouponDto coupon)
        {
            var result = _couponService.Create(coupon);
            return CreateResponse(result);
        }

        [HttpPut("{id:int}")]
        public ActionResult<CouponDto> Update(int id, [FromBody] CouponDto coupon)
        {
            // Postavljanje identifikatora
            coupon.Id = id;

            var result = _couponService.Update(coupon);
            return CreateResponse(result);
        }


        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _couponService.Delete(id);
            return CreateResponse(result);
        }
    }
}
