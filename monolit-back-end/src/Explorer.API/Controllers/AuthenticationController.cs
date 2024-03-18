using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers;

[Route("api/users")]
public class AuthenticationController : BaseApiController
{
    private readonly IAuthenticationService _authenticationService;
    private readonly IEmailService _emailService;
    public AuthenticationController(IAuthenticationService authenticationService, IEmailService emailService)
    {
        _authenticationService = authenticationService;
        _emailService = emailService;
    }

    [HttpPost]
    public ActionResult<AuthenticationTokensDto> RegisterTourist([FromBody] AccountRegistrationDto account)
    {
        var result = _authenticationService.RegisterTourist(account);
        _emailService.SendActivationEmail(account.Email, result.Value.AccessToken);
        return CreateResponse(result);
    }

    [HttpPost("login")]
    public ActionResult<AuthenticationTokensDto> Login([FromBody] CredentialsDto credentials)
    {
        var result = _authenticationService.Login(credentials);
        return CreateResponse(result);
    }

    [AllowAnonymous]
    [HttpPatch("activate/{touristId:int}")]
    public ActionResult<bool> ActivateAccount([FromRoute] int touristId, [FromBody] CredentialsDto credentialsDto)
    {
        var result = _authenticationService.ActivateAccount(touristId);
        return CreateResponse(result);
    }

    [HttpGet("forgotPassword")]
    public ActionResult<bool> ForgotPassword([FromQuery] string email)
    {
        var result = _authenticationService.ForgotPassword(email);
        if (result.IsFailed) return false;
        _emailService.SendPasswordResetEmail(email, result.Value.AccessToken);
        return true;
    }

    [Authorize(Policy= "allRolesPolicy")]
    [HttpPost("changePassword")]
    public ActionResult<bool> ChangePassword([FromBody] PasswordChangeDto passwordChangeDto)
    {
        return CreateResponse(_authenticationService.ChangePassword(passwordChangeDto));
    }
}