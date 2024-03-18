using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Public
{
    public interface IPersonService
    {
        Result<PagedResult<PersonDto>> GetPaged(int page, int pageSize);
        Result<PersonDto> Update(PersonDto profile);
        Result<PersonDto> Get(int id);
        Result<PersonDto> GetPersonByUserId(int id);
    }
}
