using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Models.Domain.Users;
using Shared.Models.Infrastructure;
using Shared.Server.Infrastructure;
using Shared.Server.Services;

namespace ApiGateway.Controllers;

[Controller]
[Authorize]
public class AuthController : Controller
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [AllowAnonymous]
    [HttpPost(WebApi.RegisterPlayer)]
    [ProducesResponseType((int) HttpStatusCode.OK)]
    [ProducesResponseType((int) HttpStatusCode.Conflict)]
    public async Task<ActionResult> RegisterPlayer(string email, string password, string name)
    {
        var response = await _authService.CreateUser(new CreateUserArgs(email, password, name, new Roles()
        {
            Roles.Player
        }));
        return response.ToActionResult();
    }

    [AllowAnonymous]
    [HttpPost(WebApi.Login)]
    [ProducesResponseType(typeof(string), (int) HttpStatusCode.OK)]
    [ProducesResponseType((int) HttpStatusCode.NotFound)]
    public async Task<ActionResult> Login(string username, string password)
    {
        var response = await _authService.GetAccessToken(new GetAccessTokenArgs(username, password));
        if (!response.IsSucceeded)
            return response.ToActionResult();

        return Json(response.Value);
    }

    [HttpPost(WebApi.RemoveUser)]
    [ProducesResponseType((int) HttpStatusCode.OK)]
    [ProducesResponseType((int) HttpStatusCode.NotFound)]
    public async Task<ActionResult> RemoveUser(string password)
    {
        return await _authService.RemoveUser(new RemoveUserArgs(User.Id(), password));
    }
}