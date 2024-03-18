using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Dtos.Tours;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.Tours;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Converters
{
    public static class ProblemConverter
    {
        public static ProblemDto ToDto(this Problem problem)
        {
            if (problem == null)
            {
                return null;
            }
            return new ProblemDto
            {
                Category = problem.Category,
                Priority = problem.Priority,
                Description = problem.Description,
                Time = problem.Time,
                TourId = (int)problem.TourId,
                TouristId = (int)problem.TouristId,
                AuthorsSolution = problem.AuthorsSolution,
                IsSolved = problem.IsSolved,
                UnsolvedProblemComment = problem.UnsolvedProblemComment,
                Deadline = problem.Deadline
            };
        }
        public static Problem ToDomain(this ProblemDto problemDto)
        {
            return problemDto == null ? null :
                new Problem(problemDto.Category, problemDto.Priority, problemDto.Description, problemDto.Time, problemDto.TourId, problemDto.TouristId, problemDto.AuthorsSolution, problemDto.IsSolved, problemDto.UnsolvedProblemComment, problemDto.Deadline);
        }
    }
}