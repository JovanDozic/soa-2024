using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos.Tours;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain.Tours;
using FluentResults;

namespace Explorer.Tours.Core.UseCases.Administration
{
    public class ProblemService : CrudService<TourDto, Tour>, IProblemService
    {
        private readonly IProblemRepository _problemRepository;
        private readonly ICrudRepository<Tour> _tourRepository;
        private readonly ITourService _tourService;

        public ProblemService(ICrudRepository<Tour> repository, IMapper mapper, IProblemRepository problemRepository, ITourService tourService) : base(repository, mapper)
        {
            _problemRepository = problemRepository;
            _tourRepository = repository;
            _tourService = tourService;
        }



        public Result<ProblemDto> RespondToProblem(int id, string response)
        {
            var allTours = _tourRepository.GetPaged(0, 0).Results;
            var problem = new ProblemDto();
            foreach (var tour in allTours)
            {
                foreach (ProblemDto p in tour.Problems)
                {
                    if (Convert.ToInt64(p.Id)== id && tour.Id == 2)
                    {
                        if (!p.IsSolved)
                        {
                            p.AuthorsSolution = response;
                        }
                        problem = p;
                        _tourRepository.Update(tour);
                        return problem;
                    }
                }
            }
            return problem;
        }
        /*
        public Result<List<ProblemDto>> GetProblemsWithExpiredDeadline(List<ProblemDto> problems)
        {
            //var unresolvedProblems = _problemRepository.GetUnresolvedProblemsWithDeadline(problems);

           // return Result.Ok(unresolvedProblems);



        }*/
        /*
        public Result<List<ProblemDto>> GetAll()
        {
            var problems = _problemRepository.GetAll();

            if (problems != null)
            {

                return Result.Ok(problems);
            }
            else
            {
                return Result.Fail<List<ProblemDto>>("Failed to retrieve problems.");
            }
        }*/

        public object ProblemNotSolved(long id, string comment)
        {
            var allTours = _tourRepository.GetPaged(0, 0).Results;
            var problem = new ProblemDto();
            foreach (var tour in allTours)
            {
                foreach (ProblemDto p in tour.Problems)
                {
                    if (Convert.ToInt64(p.Id) == id && tour.Id == 2)
                    {
                        p.IsSolved = false;
                        p.UnsolvedProblemComment = comment;
                        problem = p;
                        _tourRepository.Update(tour);
                        return problem;
                    }
                }
            }
            return problem;
        }
        public object ProblemIsSolved(long id)
        {
            var allTours = _tourRepository.GetPaged(0, 0).Results;
            var problem = new ProblemDto();
            foreach (var tour in allTours)
            {
                foreach (ProblemDto p in tour.Problems)
                {
                    if (Convert.ToInt64(p.Id) == id && tour.Id == 2)
                    {
                        p.IsSolved = true;
                        problem = p;
                        _tourRepository.Update(tour);
                        return problem;
                    }
                }
            }
            return problem;
        }
        public Result<List<ProblemDto>> GetToursProblems(int id)
        {
            var tour = _tourRepository.Get(id);
            var problem = new List<ProblemDto>();
            foreach (ProblemDto p in tour.Problems)
            {
                problem.Add(p);
            }
            
            return problem;
        }
        /*
         public object GetProblemById(long id)
         {
             var problemDto = _problemRepository.GetProblemById(id);
             return problemDto;
         }

         
           public object SetProblemDeadline(long id, DateTime newDeadline)
         {
             var problemDto = _problemRepository.GetProblemById(id);
             var problem = ProblemConverter.ToDomain(problemDto);
             problem.SetDeadline(newDeadline);
             //CrudRepository.Update(problem);
             _problemRepository.SaveChanges(problem);

             problemDto = ProblemConverter.ToDto(problem);
             //problemDto.Id = problem.Id;
             return problemDto;
         }



         public Result<ProblemDto> DeleteProblem(long Id)
         {

             var problemDto = _problemRepository.GetProblemById(Id);
             var problem = ProblemConverter.ToDomain(problemDto);
             if (problem.Deadline < DateTime.Now)
             {
                 if (problem.IsSolved)
                 {
                    // CrudRepository.Delete(problem.Id); 
                 }
                 else
                 {
                    _problemRepository.TourFromProblem(problemDto);
                    // CrudRepository.Delete(problem.Id);

                 }

             }

             problemDto = ProblemConverter.ToDto(problem);
            // problemDto.Id = problem.Id;
             return problemDto;
         }

         public Result<ProblemDto> Create(ProblemDto problem)
         {
             throw new NotImplementedException();
         }

         Result<PagedResult<ProblemDto>> IProblemService.GetPaged(int page, int pageSize)
         {
             throw new NotImplementedException();
         }*/
    }
}