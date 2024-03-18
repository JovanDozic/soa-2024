using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Explorer.Payments.Core.Domain;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using Explorer.Stakeholders.Core.Domain.Users;
using FluentResults;

namespace Explorer.Tours.Core.UseCases.Shopping
{
    public class TourPurchaseTokenService : CrudService<TourPurchaseTokenDto, TourPurchaseToken>, ITourPurchaseTokenService
    {
        private readonly ITourPurchaseTokenRepository _tourPurchaseRepository;
        private readonly IWalletRepository _walletRepository;
        public TourPurchaseTokenService(ICrudRepository<TourPurchaseToken> repository, IMapper mapper, ITourPurchaseTokenRepository tourPurchaseRepository, IWalletRepository walletRepository) : base(repository, mapper)
        {
            _tourPurchaseRepository = tourPurchaseRepository;
            _walletRepository = walletRepository;
        }

        public Result<bool> GetToken(int idUser, int idTour)
        {
            return _tourPurchaseRepository.GetToken(idUser, idTour);
        }

        public Result<List<TourPurchaseTokenDto>> PurchaseItemsFromCart(ShoppingCartDto shoppingCart)
        {
            double coins = 0;
            foreach (var item in shoppingCart.Items)
            {
                coins -= item.Price;
                if (item.Type.Equals("Bundle"))
                {
                    foreach (var tour in item.ToursInfo)
                    {
                        var token = new TourPurchaseToken(0, shoppingCart.IdUser, tour.Id, DateTime.Now.ToUniversalTime(), tour.Name, tour.Image);                      
                        CrudRepository.Create(token);
                    }
                }
                else
                {
                    var token = new TourPurchaseToken(0, shoppingCart.IdUser, item.IdType, DateTime.Now.ToUniversalTime(), item.Name, item.Image);
                    CrudRepository.Create(token);
                }
            }
            _walletRepository.AddCoinsToWallet(shoppingCart.IdUser, coins);
            return MapToDto(_tourPurchaseRepository.GetAllForUser(shoppingCart.IdUser));
        }

        public Result<List<TourPurchaseTokenDto>> GetAllForUser(int userId)
        {
            return MapToDto(_tourPurchaseRepository.GetAllForUser(userId));
        }
        public Result<int> GetToursPurchaseCount(int tourId)
        {
            var allTokens = CrudRepository.GetPaged(0, 0);
            var tokensForTour = allTokens.Results
                .Where(token => token.TourId == tourId)
                .ToList();
            return tokensForTour.Count;
        }

        public Result<int> GetAuthorsPurchasedTours()
        {
            var allTokens = CrudRepository.GetPaged(0, 0);
            var uniqueTourIdsForUser = allTokens.Results
                .Select(token => token.TourId)
                .Distinct()
                .ToList();

            return uniqueTourIdsForUser.Count;
        }
    }
}
