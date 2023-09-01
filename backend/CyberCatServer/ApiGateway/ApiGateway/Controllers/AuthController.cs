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
    [ProducesResponseType((int) HttpStatusCode.OK)]
    public async Task<ActionResult> Register(string email, string password, string name)
    {
        var response = await _authGrpcService.CreateUser(new CreateUserArgs
        {
            Email = email,
            Password = password,
            Name = name
        });

        return response;
    }

    [AllowAnonymous]
    [HttpPost("login")]
    [ProducesResponseType(typeof(string), (int) HttpStatusCode.OK)]
    public async Task<ActionResult<string>> Login(string email, string password)
    {
        var response = await _authGrpcService.GetAccessToken(new GetAccessTokenArgs
        {
            Email = email,
            Password = password
        });

        return response;
    }

    [HttpDelete]
    [ProducesResponseType((int) HttpStatusCode.OK)]
    public async Task<ActionResult> Remove([FromUser] UserId userId)
    {
        await _authGrpcService.Remove(userId);
        return Ok();
    }
}