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
public class AuthController : ControllerBase
{
    private readonly string _authServiceGrpcEndpoint;

    public AuthController(IOptions<ApiGatewayAppSettings> appSettings)
    {
        _authServiceGrpcEndpoint = appSettings.Value.ConnectionStrings.AuthServiceGrpcEndpoint;
        if (string.IsNullOrEmpty(_authServiceGrpcEndpoint))
        {
            throw new ArgumentNullException(nameof(_authServiceGrpcEndpoint));
        }
    }

    [HttpGet("authorize_player")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public ActionResult<AuthorizePlayerResponseDto> AuthorizePlayer()
    {
        return new AuthorizePlayerResponseDto
        {
            Name = User.Identity.Name
        };
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<ActionResult<TokenResponseDto>> Login(string username, string password)
    {
        using var channel = GrpcChannel.ForAddress(_authServiceGrpcEndpoint);
        var service = channel.CreateGrpcService<IAuthService>();
        var access = await service.GetAccessToken(new GetAccessTokenArgsDto
        {
            Email = username,
            Password = password
        });

        return new TokenResponseDto
        {
            AccessToken = access.Value
        };
    }
}