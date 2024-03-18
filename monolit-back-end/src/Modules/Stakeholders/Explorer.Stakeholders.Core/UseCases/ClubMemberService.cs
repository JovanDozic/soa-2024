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
    public class ClubMemberService : CrudService<ClubMemberDto, ClubMember>, IClubMemberService
    {

        public ClubMemberService(ICrudRepository<ClubMember> repository, IMapper mapper) : base(repository, mapper)
        {
           
        }



    }
}
