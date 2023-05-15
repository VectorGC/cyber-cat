using ApiGateway.Dto;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ProtoBuf.Grpc.Client;
using Shared.Dto;
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
    public async Task<ActionResult<TokenResponseDto>> Login(string username, string password)
    {
        var response = await _authService.GetAccessToken(new GetAccessTokenArgsDto
        {
            Email = username,
            Password = password
        });

        return new TokenResponseDto
        {
            AccessToken = response.AccessToken
        };
    }

    [HttpGet("authorize_player")]
    public ActionResult<AuthorizePlayerResponseDto> AuthorizePlayer()
    {
        return new AuthorizePlayerResponseDto
        {
            Name = User.Identity.Name
        };
    }
}