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
    private readonly IAuthGrpcService _authGrpcService;

    public AuthController(IAuthGrpcService authGrpcService)
    {
        _authGrpcService = authGrpcService;
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<ActionResult<TokenDto>> Login(string username, string password)
    {
        var response = await _authGrpcService.GetAccessToken(new GetAccessTokenArgs
        {
            Email = username,
            Password = password
        });

        return new TokenDto
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