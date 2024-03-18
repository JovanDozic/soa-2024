using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Explorer.BuildingBlocks.Core.Domain;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Explorer.Stakeholders.Core.UseCases;
using FluentResults;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class ClubInvitationService : CrudService<ClubInvitationDto, ClubInvitation>, IClubInvitationService
    {


        private IClubService clubServiceInstance;
        public ClubInvitationService(ICrudRepository<ClubInvitation> repository, IMapper mapper, IClubService clubService) : base(repository, mapper)
        {
            clubServiceInstance = clubService;
        }

        public bool IsInvitationOwner(int userId, int clubId)
        {
               return clubServiceInstance.IsClubOwner(userId,clubId);
       
        }


    }
}
