using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.Core.Domain;
using Explorer.Payments.Core.Domain.Order;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using FluentResults;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Payments.Infrastructure.Database.Repositories
{
    public class WalletRepository : CrudDatabaseRepository<Wallet, PaymentsContext>, IWalletRepository
    {
        private readonly PaymentsContext _context;
        private readonly DbSet<Wallet> _dbSet;
            public WalletRepository(PaymentsContext dbContext) : base(dbContext)
            {
                _context = dbContext;
                _dbSet = DbContext.Set<Wallet>();
            }

        public Result<Wallet> CreateWallet(int userId)
        {
            var userWallet = _dbSet.FirstOrDefault(x => x.UserId == userId);
            if (userWallet == null)
            {
                var wallet = new Wallet(userId, 0);
                _dbSet.Add(wallet);
                _context.SaveChanges();
                return _dbSet.FirstOrDefault(x => x.UserId == userId);
            }
            return userWallet;
            
        }

        public Result<Wallet> AddCoinsToWallet(int userId, double coins)
        {
            var wallet = _dbSet.FirstOrDefault(x => x.UserId == userId);
            wallet.AddCoins(coins);
            _dbSet.Update(wallet);
            _context.SaveChanges();
            return wallet;
        }

        public Result<Wallet> GetByUserId(int userId)
        {
            var wallet = _dbSet.FirstOrDefault(x => x.UserId == userId);
            if (wallet == null)
            {
                var newWallet = new Wallet(userId, 570);
                _dbSet.Add(newWallet);
                DbContext.SaveChanges();
                return newWallet;
            }
            return wallet;
        }
    }
}
