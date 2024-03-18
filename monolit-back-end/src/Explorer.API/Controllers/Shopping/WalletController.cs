using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Explorer.Payments.API.Public.Shopping;
using Explorer.Payments.Core.UseCases.Shopping;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Shopping
{
    //[Authorize(Policy = "touristPolicy")]
    [Route("api/tourist/wallet")]
    public class WalletController : BaseApiController
    {
        private readonly IWalletService _walletService;
        public WalletController(IWalletService orderService)
        {
            _walletService = orderService;
        }

        [HttpPost]
        public ActionResult<WalletDto> Create([FromBody] WalletDto wallet)
        {
            var result = _walletService.Create(wallet);
            return CreateResponse(result);
        }

        [HttpGet("{id:int}")]
        public ActionResult<WalletDto> GetByIdUser(int id)
        {
            var result = _walletService.GetByUserId(id);
            return CreateResponse(result);
        }

        [HttpPut]
        public ActionResult<WalletDto> Update([FromBody] WalletDto wallet)
        {
            var result = _walletService.Update(wallet);
            return CreateResponse(result);
        }

        [HttpGet("getByUserId/{userId:int}")]
        public ActionResult<WalletDto> GetByUserId(int userId)
        {
            var result = _walletService.GetByUserId(userId);
            return CreateResponse(result);
        }

        [HttpGet]
        public ActionResult<PagedResult<WalletDto>> GetPaged(int page, int pageSize)
        {
            var result = _walletService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

        [HttpPatch("addCoins/{userId:int}")]
        public ActionResult<WalletDto> AddCoinsToWallet([FromRoute]int userId, [FromBody]int coins)
        {
            var result = _walletService.AddCoinsToWallet(userId, coins);
            return CreateResponse(result);
        }
    }
}
