using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using FluentResults;

namespace Explorer.Stakeholders.API.Public
{
    public interface IClubService
    { 
        Result<ClubRegistrationDto> Create(ClubRegistrationDto reg);
        Result<ClubRegistrationDto> Update(ClubRegistrationDto reg);
       // ClubRegistrationDto MemberExist(ClubRegistrationDto club,int id);
        bool IsClubOwner(int userId,int clubId);
        Result<PagedResult<ClubRegistrationDto>> GetPaged(int page, int pageSize);
    }
}
