using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using FluentResults;

namespace Explorer.Payments.Core.Domain.RepositoryInterfaces
{
    public interface IWalletRepository : ICrudRepository<Wallet>
    {
        Result<Wallet> CreateWallet(int userId);
        Result<Wallet> AddCoinsToWallet(int userId, double coins);
        Result<Wallet> GetByUserId(int userId);
    }
}
