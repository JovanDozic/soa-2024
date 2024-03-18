using AutoMapper;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.Users;

namespace Explorer.Stakeholders.Core.Mappers;

public class StakeholderProfile : Profile
{
    public StakeholderProfile()
    {
        CreateMap<UserDto, User>().ReverseMap();
        CreateMap<FollowerDto, Follower>().ReverseMap();
        CreateMap<NotificationDto, Notification>().ReverseMap();

        CreateMap<AppRatingDto, AppRating>().ReverseMap();
        CreateMap<ClubRegistrationDto, Club>().ReverseMap();
        CreateMap<ClubInvitationDto, ClubInvitation>().ReverseMap();
        CreateMap<MembershipRequestDto, MembershipRequest>().ReverseMap();
        CreateMap<ClubMemberDto, ClubMember>().ReverseMap();

    }
}