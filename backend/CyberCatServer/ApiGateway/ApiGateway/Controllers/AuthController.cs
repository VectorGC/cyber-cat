using System.Net;
using ApiGateway.Attributes;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Server.Ids;
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
    [HttpPut("signUp")]
    [ProducesResponseType((int) HttpStatusCode.OK)]
    public async Task<ActionResult> SignUp(string email, string password, string name)
    {
        return await _authGrpcService.CreateUser(new CreateUserArgs(email, password, name));
    }

    [AllowAnonymous]
    [HttpPost("signIn")]
    [ProducesResponseType(typeof(string), (int) HttpStatusCode.OK)]
    public async Task<ActionResult<string>> SignIn(string email, string password)
    {
        return await _authGrpcService.GetAccessToken(new GetAccessTokenArgs(email, password));
    }

    [HttpDelete]
    [ProducesResponseType((int) HttpStatusCode.OK)]
    public async Task<ActionResult> Remove([FromUser] UserId userId, string password)
    {
        return await _authGrpcService.Remove(new RemoveArgs(userId, password));
    }
}