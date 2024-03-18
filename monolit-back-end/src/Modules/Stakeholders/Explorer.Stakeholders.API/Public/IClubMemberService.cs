using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using FluentResults;

namespace Explorer.Stakeholders.API.Public;

public interface IClubMemberService
{
    Result<ClubMemberDto> Create(ClubMemberDto clubMember);
   // bool IsInvitationOwner(int userId, int clubId);
    Result Delete(int id);
    Result<PagedResult<ClubMemberDto>> GetPaged(int page, int pageSize);
}