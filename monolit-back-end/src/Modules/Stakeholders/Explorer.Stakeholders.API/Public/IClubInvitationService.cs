using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using FluentResults;

namespace Explorer.Stakeholders.API.Public;

public interface IClubInvitationService
{
   
    Result<ClubInvitationDto> Create(ClubInvitationDto clubInvitation);
    bool IsInvitationOwner(int userId,int clubId);
    Result Delete(int id);
    Result<PagedResult<ClubInvitationDto>> GetPaged(int page, int pageSize);
}