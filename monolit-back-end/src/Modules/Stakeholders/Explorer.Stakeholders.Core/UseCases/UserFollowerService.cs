using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain.Users;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class UserFollowerService : CrudService<UserDto, User>, IUserFollowerService
    {
        public UserFollowerService(ICrudRepository<User> repository, IMapper mapper) : base(repository, mapper)
        {

        }

        public Result<UserDto> Follow(int userId, int userToFollowId)
        {
            var user = CrudRepository.Get(userId);
            var userToFollow = CrudRepository.Get(userToFollowId);

            if (user is null || userToFollow is null)
            {
                return Result.Fail<UserDto>($"User not found ({FailureCode.NotFound}).");
            }

            try
            { 
                userToFollow.AddFollower(user);
                CrudRepository.Update(userToFollow);

                return MapToDto(userToFollow);
            }
            catch (Exception ex)
            {
                return Result.Fail<UserDto>(ex.Message);
            }

        }

        public Result<List<UserDto>> GetAll()
        {
            try
            {
                var users = CrudRepository.GetPaged(1, int.MaxValue);
                List<UserDto> result = new List<UserDto>();
                foreach (var user in users.Results)
                {
                       result.Add(new()
                       {
                        Id = user.Id,
                        Username = user.Username,
                        Password = user.Password,
                        Role = user.Role,
                        IsActive = user.IsActive,
                        Followers = user.Followers.Select(f => new FollowerDto
                        {
                            UserId = f.UserId,
                            Username = f.Username,
                            Date = f.Date
                        }).ToList()
                    });
                }
                return result;
            }
            catch (Exception ex)
            {
                return Result.Fail<List<UserDto>>(ex.Message);
            }
        }

        public Result<List<FollowerDto>> GetFollowers(int userId)
        {
            var user = CrudRepository.Get(userId);

            if (user is null)
            {
                return Result.Fail<List<FollowerDto>>($"User not found ({FailureCode.NotFound}).");
            }

            try
            {
                var followers = user.Followers.ToList();
                List<FollowerDto> result = new List<FollowerDto>();

                foreach (var follower in followers)
                {
                    result.Add(new()
                    {
                        UserId = follower.UserId,
                        Username = follower.Username,
                        Date = follower.Date,
                    });
                }

                return result;
            }
            catch (Exception ex)
            {
                return Result.Fail<List<FollowerDto>>(ex.Message);
            }
        }

        public Result<UserDto> Unfollow(int userId, int userToUnfollowId)
        {
            var user = CrudRepository.Get(userId);
            var userToUnfollow = CrudRepository.Get(userToUnfollowId);

            if (user is null || userToUnfollow is null)
            {
                return Result.Fail<UserDto>($"User not found ({FailureCode.NotFound}).");
            }

            try
            {
                userToUnfollow.RemoveFollower(user);
                CrudRepository.Update(userToUnfollow);

                return MapToDto(userToUnfollow);
            }
            catch (Exception ex)
            {
                return Result.Fail<UserDto>(ex.Message);
            }
        }
    }
}