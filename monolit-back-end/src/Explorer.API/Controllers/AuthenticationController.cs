using System.Text;
using Explorer.Blog.Core.Domain;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Tours.API.Dtos.Tours;
using Explorer.Tours.Core.Domain.Tours;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Explorer.API.Controllers;

[Route("api/users")]
public class AuthenticationController : BaseApiController
{
    private readonly IAuthenticationService _authenticationService;
    private readonly IEmailService _emailService;
    private readonly string _gatewayUrl = "http://localhost:8084";
    static readonly HttpClient _client = new();
    public AuthenticationController(IAuthenticationService authenticationService, IEmailService emailService)
    {
        _authenticationService = authenticationService;
        _emailService = emailService;
    }

    /*[HttpPost]
    public ActionResult<AuthenticationTokensDto> RegisterTourist([FromBody] AccountRegistrationDto account)
    {
        var result = _authenticationService.RegisterTourist(account);
        _emailService.SendActivationEmail(account.Email, result.Value.AccessToken);
        return CreateResponse(result);
    }*/

    /*[HttpPost("login")]
    public ActionResult<AuthenticationTokensDto> Login([FromBody] CredentialsDto credentials)
    {
        var result = _authenticationService.Login(credentials);
        return CreateResponse(result);
    }*/

    [HttpPost("login")]
    public async Task<ActionResult<AuthenticationTokensDto>> Login([FromBody] CredentialsDto credentials)
    {
        string uri = $"{_gatewayUrl}/login";
        var settings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };
        string userJson = JsonConvert.SerializeObject(credentials, settings);
        HttpContent httpContent = new StringContent(userJson, Encoding.UTF8, "application/json");
        using HttpResponseMessage response = await _client.PostAsync(uri, httpContent);

        if (!response.IsSuccessStatusCode)
        {
            return StatusCode((int)response.StatusCode);
        }

        string content = await response.Content.ReadAsStringAsync();
        return CreateResponse(content.ToResult());
    }

    [HttpPost]
    public async Task<ActionResult<AuthenticationTokensDto>> Register([FromBody] AccountRegistrationDto account)
    {
        string uri = $"{_gatewayUrl}/register";
        var settings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };
        string userJson = JsonConvert.SerializeObject(account, settings);
        HttpContent httpContent = new StringContent(userJson, Encoding.UTF8, "application/json");
        using HttpResponseMessage response = await _client.PostAsync(uri, httpContent);

        if (!response.IsSuccessStatusCode)
        {
            return StatusCode((int)response.StatusCode);
        }

        string content = await response.Content.ReadAsStringAsync();
        return CreateResponse(content.ToResult());
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