using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using FluentResults;

namespace Explorer.Payments.API.Public
{
    public interface IWalletService
    {
        Result<WalletDto> Create(WalletDto walletDto);
        Result<WalletDto> CreateWallet(int userId);
        Result<WalletDto> Update(WalletDto walletDto);
        Result<WalletDto> AddCoinsToWallet(int userId, int coins);
        Result<WalletDto> GetByUserId(int userId);
        Result<PagedResult<WalletDto>> GetPaged(int page, int pageSize);
    }
}
