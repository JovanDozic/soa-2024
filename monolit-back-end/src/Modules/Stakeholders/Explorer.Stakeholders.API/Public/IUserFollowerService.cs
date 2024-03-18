using Explorer.Stakeholders.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Public
{
    public interface IUserFollowerService
    {
        public Result<UserDto> Follow(int followerId, int userToFollowId);
        public Result<List<FollowerDto>> GetFollowers(int userId);
        public Result<UserDto> Unfollow(int userId, int userToUnfollowId);
        public Result<List<UserDto>> GetAll();
    }
}
