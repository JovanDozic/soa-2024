using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.Core.Domain.Users;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Tours.API.Dtos.Tours;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Converters;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain.TourExecutions;
using Explorer.Tours.Core.Domain.Tours;
using FluentResults;
using System.Xml.Linq;
using Explorer.Stakeholders.Core.UseCases;
using Explorer.Stakeholders.API.Public;
using System.Linq;

namespace Explorer.Tours.Core.UseCases.Administration
{
    public class TourService : CrudService<TourDto,Tour>, ITourService
    {
        
        private readonly IProblemRepository _problemRepository;
        private readonly ICrudRepository<TourExecution> _tourExecutionRepository;
        private readonly IUserFollowerService _userFollowerService;
        public TourService(ICrudRepository<Tour> repository, IProblemRepository problemRepository, ICrudRepository<TourExecution> tourExecutionRepository, IUserFollowerService userFollowerService, IMapper mapper) : base(repository, mapper) 
        {
            _tourExecutionRepository = tourExecutionRepository;
            _problemRepository = problemRepository;
            _userFollowerService = userFollowerService;
        }

        public Result<TourDto> PublishTour(long id)
        {
            var tourDb = CrudRepository.Get(id);
            tourDb.PublishTour();
            CrudRepository.Update(tourDb);
            return MapToDto(tourDb);
        }

        public Result<TourDto> ArhiveTour(long id)
        {
            var tourDb = CrudRepository.Get(id);
            tourDb.ArhiveTour();
            CrudRepository.Update(tourDb);
            return MapToDto(tourDb);
        }

        public Result<TourDto> AddProblem(int tourId,ProblemDto problemDto)
        {
            //var problem = ProblemConverter.ToDomain(problemDto);
            Tour tour = CrudRepository.Get(tourId);
            if (tour is null)
            {
                return Result.Fail<TourDto>($"Tour not found ({FailureCode.NotFound}).");
            }
            try
            {
                var count = tour.Problems.Count();
                problemDto.Id = count + 1;
                tour.Problems.Add(problemDto);
                CrudRepository.Update(tour);

                return MapToDto(tour);
            }
            catch (Exception ex)
            {
                return Result.Fail<TourDto>(ex.Message);
            }
        }

        /*public Result<TourDto> RespondToProblem(int tourId, ProblemDto problem)
        {
           var problemDomain = ProblemConverter.ToDomain(problem);
           var oldTour = CrudRepository.Get(tourId);
           oldTour.RespondToProblem(problemDomain);
           CrudRepository.Update(oldTour);
           return MapToDto(oldTour);
        }*/
        //za update

        public Result<List<TourDto>> GetAllPublic()
        {
            var allTours = CrudRepository.GetPaged(0, 0).Results;
            var toursWithPublicPoints = new List<TourDto>();

            foreach(var tour in allTours)
            {
                var publicPoints = tour.Points.Where(point => point.Public).ToList();

                var tourDto = MapToDto(tour);
                tourDto.Points = publicPoints.Select(PointConverter.ToDto).ToList();    
                toursWithPublicPoints.Add(tourDto);
            }

            return toursWithPublicPoints;
        }

        public Result<List<TourDto>> SearchByPointDistance(double longitude, double latitude, int distance)
        {
            var searchResults = new List<TourDto>();
            foreach (var tour in CrudRepository.GetPaged(0, 0).Results.Where(x => x.HasPointsWithinDistance(longitude, latitude, distance)))
            {
                searchResults.Add(MapToDto(tour));
            }
            return searchResults;
        }
        public Result<TourDto> PublishPoint(long id, string pointName)
        {
            var tourDb = CrudRepository.Get(id);
            tourDb.PublishPoint(pointName);
            CrudRepository.Update(tourDb);
            return MapToDto(tourDb);
        }
    
          
        public Result<TourDto> RateTour(int tourId, TourReviewDto review)
        {
            var tourReview = TourReviewConverter.ToDomain(review);
            Tour tour = CrudRepository.Get(tourId);

            bool hasReviewWithUserId = tour.Reviews.Any(r => r.TouristId == review.TouristId);

            var sortedTourExecutions = _tourExecutionRepository.GetPaged(0, 0).Results.Where(te => te.TourId == tourId).OrderByDescending(te => te.Id).ToList();

            TourExecution tourExecution = sortedTourExecutions.FirstOrDefault();
            if (tourExecution == null) return Result.Fail("You must start the tour.");

            double percentageOfDone = tourExecution.PercentageOfDone(tourExecution);
            bool isLastActivityBad=tourExecution.IsLastActivityWithinWeek(tourExecution);
            if (percentageOfDone > 35.0 && !isLastActivityBad )
            {
                if (!hasReviewWithUserId) {
                    tour.Reviews.Add(tourReview);
                    CrudRepository.Update(tour);

                    return MapToDto(tour); }
                else return Result.Fail("You already gave a review.");
            }

            return Result.Fail("You must complete more than 35% of tour in the last 7 days.");
        }

        public Result<double> GetAverageRating(int tourId)
        {
            Tour tour = CrudRepository.Get(tourId);
            return tour.GetAverageRating();
        }

        public Result<TourDto> GetById(long id)
        {
            var tourDb = CrudRepository.Get(id);

            var publicPoints = tourDb.Points.Where(point => point.Public).ToList();
            var tourDto = MapToDto(tourDb);
            tourDto.Points = publicPoints.Select(PointConverter.ToDto).ToList();

            return tourDto;
        }




        public Result<List<TourDto>> FindToursContainingPoints(List<PointDto> pointsToFind)
        {
            if (pointsToFind.Count < 2)
            {
                return Result.Fail("Tour must contain at least 2 points.");
                
            }

            var searchResults = new List<TourDto>();
            var allTours = CrudRepository.GetPaged(0, 0).Results;
         
           
                foreach (var tour in allTours)
            { if ((bool)tour.MyOwn==false) 
                {
                    var tourPoints = tour.Points.Select(PointConverter.ToDto).ToList();


                    bool containsAllPoints = pointsToFind.All(point => tourPoints.Any(tourPoint => tour.AreEqualPoints(point, tourPoint)));

                    if (containsAllPoints)
                    {
                        searchResults.Add(MapToDto(tour));
                    }
                }
            }

            return searchResults;
        }


        public Result<List<PointDto>> GetAllPublicPointsForTours()
        {
            var allTours = CrudRepository.GetPaged(0, 0).Results;
            var uniquePublicPoints = new HashSet<PointDto>();

            foreach (var tour in allTours)
            {
                if ((bool)tour.MyOwn==false)
                {
                    var publicPoints = tour.Points.Where(point => point.Public);
                    foreach (var point in publicPoints)
                    {
                        var pointDto = PointConverter.ToDto(point);
                        if (!uniquePublicPoints.Any(existingPoint => tour.AreEqualPoints(existingPoint, pointDto)))
                        {
                            uniquePublicPoints.Add(pointDto);
                        }
                    }
                }
            }

            return uniquePublicPoints.ToList();
        }


        public Result<List<TourDto>> GetToursReviewedByUsersIFollow(int currentUserId, int ratedTourId)
        {
            var allUsersResult = _userFollowerService.GetAll();
            if (!allUsersResult.IsSuccess)
            {
                return Result.Fail<List<TourDto>>("Unable to retrieve users.");
            }

            var usersIFollowIds = allUsersResult.Value
                                    .Where(user => user.Followers.Any(follower => follower.UserId == currentUserId))
                                    .Select(user => user.Id)
                                    .ToList();


            var allTours = GetAllPublic().Value;

            var ratedToursByCurrentUser = allTours
                                    .Where(tour => tour.Reviews.Any(review => review.TouristId == currentUserId))
                                    .Select(tour => tour.Id)
                                    .ToList();

            List<int> usersWhoReviewedSameTour = new List<int>();
            var ratedTour = allTours.FirstOrDefault(tour => tour.Id == ratedTourId);
            if (ratedTour != null)
            {
                foreach (var review in ratedTour.Reviews)
                {
                    if (usersIFollowIds.Contains(review.TouristId))
                    {
                        usersWhoReviewedSameTour.Add(review.TouristId);
                    }
                }
            }

            var filteredTours = allTours.Where(tour =>
                tour.Reviews.Any(review => usersWhoReviewedSameTour.Contains(review.TouristId)) &&
                !ratedToursByCurrentUser.Contains(tour.Id) &&
                tour.Id != ratedTourId).ToList();

            return filteredTours;
            
        }

        public Result<long> GetIdByName(string name)
        {
            return CrudRepository.GetPaged(0, 0).Results.FirstOrDefault(x => x.Name.Equals(name)).Id;
        }
        public Result<List<TourDto>> GetAllAuthorsTours(int idUser)
        {
            var allTours = CrudRepository.GetPaged(0, 0).Results;

            var tours = new List<TourDto>();
            foreach(var tour in allTours)
            {
                if(tour.AuthorId == idUser)
                {
                    
                    tours.Add(MapToDto(tour));
                }
            }

            return tours;
        }

        public Result<TourDto> AddProblem(long tourId, ProblemDto problem)
        {
            throw new NotImplementedException();
        }

        public Result<List<ProblemDto>> GetAllProblems(int authorsId)
        {
            var allTours = CrudRepository.GetPaged(0, 0).Results;
            List<ProblemDto> problems = new List<ProblemDto>();
            foreach (var tour in allTours)
            {
                if (tour.AuthorId == authorsId)
                {
                    foreach(ProblemDto problem in tour.Problems)
                    { 
                        problems.Add(problem);
                    }
                }
            }
            return problems;
        }
        public Result<List<ProblemDto>> GetAllTouristsProblems(int touristsId)
        {
            var allTours = CrudRepository.GetPaged(0, 0).Results;
            List<ProblemDto> problems = new List<ProblemDto>();
            foreach (var tour in allTours)
            {
                foreach (ProblemDto problem in tour.Problems)
                {
                    if(problem.TouristId == touristsId)
                    {
                        problems.Add(problem);
                    }
                }
            }
            return problems;
        }
    }
}
