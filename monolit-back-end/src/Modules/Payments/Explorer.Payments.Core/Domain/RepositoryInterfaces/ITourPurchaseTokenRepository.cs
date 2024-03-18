using Explorer.Payments.API.Dtos;
using FluentResults;

namespace Explorer.Payments.Core.Domain.RepositoryInterfaces
{
    public interface ITourPurchaseTokenRepository
    {
        Result<List<TourPurchaseToken>> GetAllForUser(int userId);
        Result<bool> GetToken(int idUser, int idTour);
    }
}
