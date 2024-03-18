using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Explorer.Stakeholders.Core.Domain.Users;
using FluentResults;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class UserService : BaseService<UserDto, User>, IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IMapper mapper, IUserRepository userRepository) : base(mapper)
        {
            _userRepository = userRepository;
        }

        public Result<UserDto> Block(string username)
        {
            try
            {
                var user = _userRepository.GetActiveByName(username);
                if (user == null) return Result.Fail("User not found");
                user.IsActive = false;
                _userRepository.Update(user);
                return MapToDto(user);
            }
            catch (Exception e)
            {
                return Result.Fail($"Error blocking the user: {e.Message}");
            }
        }

        public Result<List<UserDto>> GetAll()
        {
            var users = _userRepository.GetAll();
            return MapToDto(users);
        }

        public Result<UserDto> DisableBlogs(int id)
        {
            try
            {
                var user = _userRepository.GetAll().Find(u => u.Id == id);
                if (user.IsBlogEnabled == false) return MapToDto(user);
                user.IsBlogEnabled = false;
                _userRepository.Update(user);
                return MapToDto(user);
            }
            catch (Exception e)
            {
                return Result.Fail($"Error disabling blogs for the user: {e.Message}");
            }
        }

        public Result<bool> CanUserUseBlog(int id)
        {
            try
            {
                var user = _userRepository.GetAll().Find(u => u.Id == id);
                if (user == null) return Result.Fail("User not found");
                return user.IsBlogEnabled ?? true;
            }
            catch (Exception e)
            {
                return Result.Fail($"Error disabling blogs for the user: {e.Message}");
            }
        }
    }
}
