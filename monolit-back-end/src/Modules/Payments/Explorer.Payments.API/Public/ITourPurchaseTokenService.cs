using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using FluentResults;

namespace Explorer.Payments.API.Public
{
    public interface ITourPurchaseTokenService
    {
        Result<PagedResult<TourPurchaseTokenDto>> GetPaged(int page, int pageSize);
        Result<TourPurchaseTokenDto> Get(int id);
        Result<TourPurchaseTokenDto> Update(TourPurchaseTokenDto tourPurchaseToken);
        Result Delete(int id);
        Result<bool> GetToken(int idUser, int idTour);
        Result<List<TourPurchaseTokenDto>> PurchaseItemsFromCart(ShoppingCartDto shoppingCart);
        Result<int> GetToursPurchaseCount(int tourId);
        Result<int> GetAuthorsPurchasedTours();
        Result<List<TourPurchaseTokenDto>> GetAllForUser(int userId);
    }
}
