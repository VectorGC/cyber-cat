using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Models.Dto.Args;
using Shared.Server.Services;

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
    public async Task<ActionResult<string>> Login(string email, string password)
    {
        var response = await _authGrpcService.GetAccessToken(new GetAccessTokenArgs
        {
            Email = email,
            Password = password
        });

        return response.AccessToken;
    }

    [HttpGet("authorize_player")]
    public ActionResult<string> AuthorizePlayer()
    {
        return User.Identity.Name;
    }
}