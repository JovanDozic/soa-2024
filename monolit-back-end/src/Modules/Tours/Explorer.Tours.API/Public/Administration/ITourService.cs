using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos.Tours;
using FluentResults;
using System.Drawing;

namespace Explorer.Tours.API.Public.Administration
{
    public interface ITourService
    {
        Result<PagedResult<TourDto>> GetPaged(int page,  int pageSize);
        Result<TourDto> Create(TourDto dataIn);
        Result<TourDto> Update(TourDto dataIn);
        Result Delete(int id);
        Result<TourDto> PublishTour(long id);
        Result<TourDto> ArhiveTour(long id);
        Result<List<TourDto>> SearchByPointDistance(double longitude, double latitude, int distance);
        Result<TourDto> AddProblem(int tourId,ProblemDto problem);
        Result<TourDto> PublishPoint(long id, string pointName);
        Result<TourDto> RateTour(int tourId, TourReviewDto review);
        Result<double> GetAverageRating(int tourId);
        Result<TourDto> Get(int id);
        Result<TourDto> GetById(long id);
        Result<List<TourDto>> GetAllPublic();
        Result<List<PointDto>> GetAllPublicPointsForTours();
        Result<List<TourDto>> FindToursContainingPoints(List<PointDto> pointsToFind);
        Result<List<TourDto>> GetToursReviewedByUsersIFollow(int currentUserId, int ratedTourId);
        Result<long> GetIdByName(string name);
        Result<List<TourDto>> GetAllAuthorsTours(int idUser);
        Result<List<ProblemDto>> GetAllProblems(int authorsId);
        Result<List<ProblemDto>> GetAllTouristsProblems(int touristsId);
    }

}
