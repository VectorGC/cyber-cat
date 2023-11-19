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
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [AllowAnonymous]
    [HttpPost("signUp")]
    [ProducesResponseType((int) HttpStatusCode.OK)]
    public async Task<ActionResult> SignUp(string email, string password, string name)
    {
        var response = await _authService.CreateUser(new CreateUserArgs(email, password, name));
        return response.ToActionResult();
    }

    [AllowAnonymous]
    [HttpPost("signIn")]
    [ProducesResponseType(typeof(string), (int) HttpStatusCode.OK)]
    public async Task<ActionResult<string>> SignIn(string email, string password)
    {
        return await _authService.GetAccessToken(new GetAccessTokenArgs(email, password));
    }

    [HttpPost("remove")]
    [ProducesResponseType((int) HttpStatusCode.OK)]
    public async Task<ActionResult> Remove([FromUser] UserId userId, string password)
    {
        return await _authService.Remove(new RemoveArgs(userId, password));
    }
}