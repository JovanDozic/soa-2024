using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Dtos.Tours;
using Explorer.Tours.API.Public.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Author
{
    [Route("api/author/sale")]
    public class SaleController : BaseApiController
    {
        private readonly ISaleService _saleService;

        public SaleController(ISaleService saleService)
        {
            _saleService = saleService;
        }

        //[Authorize(Policy = "authorPolicy")]
        [HttpPost]
        public ActionResult<SaleDto> Create([FromBody] SaleDto sale)
        {
            var result = _saleService.Create(sale);
            return CreateResponse(result);
        }

        [HttpDelete("delete/{id:int}")]
        public ActionResult Delete(int id)
        {
            return CreateResponse(_saleService.Delete(id));
        }

        [HttpGet("activate/{id}")]
        public ActionResult<SaleDto> Activate(long id)
        {
            return CreateResponse(_saleService.Activate(id));
        }

        [HttpGet("getAll")]
        public ActionResult<PagedResult<SaleDto>> GetAll()
        {
            var page = 1;
            var pageSize = 20;
            return CreateResponse(_saleService.GetPaged(page, pageSize));
        }

        [HttpGet("getAllToursOnSale")]
        public ActionResult<List<TourDto>> GetAllToursOnSale()
        { 
            try
            {
                var result = _saleService.GetAllToursOnSale();
                return Ok(result); // Return 200 OK with the list of tours
            }
            catch (Exception ex)
            {
                // Log the exception or handle it accordingly
                return StatusCode(500, "An error occurred while retrieving tours on sale.");
            }
        }
    }
}
