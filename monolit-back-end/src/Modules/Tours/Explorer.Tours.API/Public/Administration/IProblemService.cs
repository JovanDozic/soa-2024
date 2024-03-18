using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos.Tours;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Public.Administration
{
    public interface IProblemService
    {/*  
        Result<List<ProblemDto>> GetProblemsWithExpiredDeadline(List<ProblemDto> problems);
        Result<List<ProblemDto>> GetAll();
        object GetProblemById(long id);
           object SetProblemDeadline(long id, DateTime deadline);
        Result<ProblemDto> DeleteProblem(long id);*/
        Result<ProblemDto> RespondToProblem(int id, string response);
        object ProblemNotSolved(long id, string comment);
        object ProblemIsSolved(long id);
        Result<List<ProblemDto>> GetToursProblems(int id);
    }
}
