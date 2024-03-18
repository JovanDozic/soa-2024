using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using Explorer.Payments.Core.Domain;
using FluentResults;
using Explorer.Payments.Core.Domain.Order;

namespace Explorer.Payments.Core.UseCases.Shopping
{
    public class WalletService : CrudService<WalletDto, Wallet>, IWalletService
    {
        private readonly IWalletRepository _walletRepository;
        private readonly IMapper _mapper;
        public WalletService(ICrudRepository<Wallet> repository, IMapper mapper, IWalletRepository walletRepository) : base(repository, mapper)
        {
            _walletRepository = walletRepository;
        }

        public Result<WalletDto> CreateWallet(int userId)
        {
            var wallet = _walletRepository.CreateWallet(userId);
            return MapToDto(wallet.Value);
        }

        public Result<WalletDto> AddCoinsToWallet(int userId, int coins)
        {
            var wallet = _walletRepository.AddCoinsToWallet(userId, coins);
            return MapToDto(wallet.Value);
        }

        public Result<WalletDto> GetByUserId(int userId)
        {
            var wallet = _walletRepository.GetByUserId(userId);
            return MapToDto(wallet.Value);
        }
    }
}
