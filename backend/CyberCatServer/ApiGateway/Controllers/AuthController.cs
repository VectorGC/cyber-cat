using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Dto;
using Shared.Dto.Args;
using Shared.Services;

namespace ApiGateway.Controllers;

[Controller]
[Route("[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class AuthController : ControllerBase
{
    private readonly IAuthGrpcService _authService;

    public AuthController(IAuthGrpcService authService)
    {
        _authService = authService;
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<ActionResult<TokenResponse>> Login(string username, string password)
    {
        var response = await _authService.GetAccessToken(new GetAccessTokenArgs
        {
            Email = username,
            Password = password
        });

        return new TokenResponse
        {
            AccessToken = response.AccessToken
        };
    }

    [HttpGet("authorize_player")]
    public ActionResult<string> AuthorizePlayer()
    {
        return User.Identity.Name;
    }
}