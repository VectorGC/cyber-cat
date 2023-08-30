using System.Net;
using ApiGateway.Attributes;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Models.Dto.Args;
using Shared.Server.Dto.Args;
using Shared.Server.Models;
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
    [HttpPost("register")]
    [ProducesResponseType(typeof(string), (int) HttpStatusCode.OK)]
    public async Task<ActionResult<string>> Login(string email, string password, string name)
    {
        await _authGrpcService.CreateUser(new CreateUserArgs
        {
            Email = email,
            Password = password,
            Name = name
        });

        return Ok();
    }

    [AllowAnonymous]
    [HttpPost("login")]
    [ProducesResponseType(typeof(string), (int) HttpStatusCode.OK)]
    public async Task<ActionResult<string>> Login(string email, string password)
    {
        var accessToken = await _authGrpcService.GetAccessToken(new GetAccessTokenArgs
        {
            Email = email,
            Password = password
        });

        return accessToken.Value;
    }

    [HttpDelete]
    [ProducesResponseType((int) HttpStatusCode.OK)]
    public async Task<ActionResult> Remove([FromUser] UserId userId)
    {
        await _authGrpcService.Remove(userId);

        return Ok();
    }
}