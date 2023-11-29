using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Models.Infrastructure;
using Shared.Server.Infrastructure;
using Shared.Server.Services;

namespace ApiGateway.Controllers;

[Controller]
//[Route("[controller]")]
[Authorize]
public class AuthController : Controller
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [AllowAnonymous]
    [HttpPost("auth/register")]
    [ProducesResponseType((int) HttpStatusCode.OK)]
    public async Task<ActionResult> Register(string email, string password, string name)
    {
        return Conflict(new {message = "asdqwe"});
        var response = await _authService.CreateUser(new CreateUserArgs(email, password, name));
        return response.ToActionResult();
    }

    [AllowAnonymous]
    [HttpPost(WebApi.Login)]
    [ProducesResponseType(typeof(string), (int) HttpStatusCode.OK)]
    public async Task<JsonResult> Login(string username, string password)
    {
        var response = await _authService.GetAccessToken(new GetAccessTokenArgs(username, password));
        return Json(response.Value);
    }

    [HttpPost("auth/remove")]
    [ProducesResponseType((int) HttpStatusCode.OK)]
    public async Task<ActionResult> Remove(string password)
    {
        return await _authService.RemoveUser(new RemoveUserArgs(User.Id(), password));
    }
}