using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Explorer.Stakeholders.Core.Domain.Users;
using FluentResults;
using static Explorer.Stakeholders.API.Enums.UserEnums;

namespace Explorer.Stakeholders.Core.UseCases;

public class AuthenticationService : IAuthenticationService
{
    private readonly ITokenGenerator _tokenGenerator;
    private readonly IUserRepository _userRepository;
    private readonly ICrudRepository<Person> _personRepository;

    public AuthenticationService(IUserRepository userRepository, ICrudRepository<Person> personRepository, ITokenGenerator tokenGenerator)
    {
        _tokenGenerator = tokenGenerator;
        _userRepository = userRepository;
        _personRepository = personRepository;
    }

    public Result<AuthenticationTokensDto> Login(CredentialsDto credentials)
    {
        var user = _userRepository.GetActiveByName(credentials.Username);
        if (user == null || !user.VerifyPassword(credentials.Password)) return Result.Fail(FailureCode.NotFound);

        long personId;
        try
        {
            personId = _userRepository.GetPersonId(user.Id);
        }
        catch (KeyNotFoundException)
        {
            personId = 0;
        }
        return _tokenGenerator.GenerateAccessToken(user, personId);
    }
    public Result<AuthenticationTokensDto> RegisterTourist(AccountRegistrationDto account)
    {
        if (_userRepository.Exists(account.Username)) return Result.Fail(FailureCode.NonUniqueUsername);

        try
        {
            var user = _userRepository.Create(new User(account.Username, account.Password, UserRole.Tourist, true, account.Email, new(), new(), isProfileActivated: false));
            var person = _personRepository.Create(new Person(user.Id, account.Name, account.Surname, " ", " ", " "));

            return _tokenGenerator.GenerateAccessToken(user, person.Id);
        }
        catch (ArgumentException e)
        {
            return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            // There is a subtle issue here. Can you find it?
        }
    }

    public Result<bool> ActivateAccount(int id)
    {
        return _userRepository.ActivateAccount(id);
    }

    public Result<AuthenticationTokensDto> ForgotPassword(string email)
    {
        if (!_userRepository.ExistsByEmail(email)) return Result.Fail(FailureCode.NotFound);

        try
        {
            var user = _userRepository.GetActiveByEmail(email) ?? throw new ArgumentException("Not found");
            return _tokenGenerator.GeneratePasswordResetToken(user);
        }
        catch (ArgumentException e)
        {
            return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
        }
    }
    public Result<bool> ChangePassword(PasswordChangeDto passwordChangeDto)
    {
        try
        {
            var user = _userRepository.GetActiveByEmail(passwordChangeDto.Email) ?? throw new ArgumentException("Not found");
            var result = user.ChangePassword(passwordChangeDto.NewPassword);
            if (result) _userRepository.Update(user);
            return result;
        }
        catch (Exception e)
        {
            return Result.Fail(FailureCode.Forbidden).WithError(e.Message);
        }
    }
}