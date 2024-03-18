using Explorer.Stakeholders.API.Dtos;
using FluentResults;

namespace Explorer.Stakeholders.API.Public;

public interface IAuthenticationService
{
    Result<AuthenticationTokensDto> Login(CredentialsDto credentials);
    Result<AuthenticationTokensDto> RegisterTourist(AccountRegistrationDto account);
    Result<bool> ActivateAccount(int id);
    Result<AuthenticationTokensDto> ForgotPassword(string email);
    Result<bool> ChangePassword(PasswordChangeDto  passwordChangeDto);

}