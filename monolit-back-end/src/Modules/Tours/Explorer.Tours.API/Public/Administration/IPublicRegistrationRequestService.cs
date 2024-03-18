using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Public.Administration
{
    public interface IPublicRegistrationRequestService
    {
        Result<PagedResult<PublicRegistrationRequestDto>> GetPaged(int page, int pageSize);
        Result<PublicRegistrationRequestDto> Create(PublicRegistrationRequestDto publicRegistrationRequest);
        Result<PublicRegistrationRequestDto> Update(PublicRegistrationRequestDto publicRegistrationRequest);
        Result Delete(int id);
        Result<List<PublicRegistrationRequestDto>> GetAllPendingRequests();
    }
}
