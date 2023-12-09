using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Models.Domain.Users;
using Shared.Models.Infrastructure;
using Shared.Server.Application.Services;
using Shared.Server.Infrastructure;

namespace ApiGateway.Controllers;

[Controller]
[Authorize]
public class AuthController : Controller
{
    private readonly IAuthService _authService;
    private readonly IPlayerService _playerService;

    public AuthController(IAuthService authService, IPlayerService playerService)
    {
        _playerService = playerService;
        _authService = authService;
    }

    [AllowAnonymous]
    [HttpPost(WebApi.RegisterPlayer)]
    [ProducesResponseType((int) HttpStatusCode.OK)]
    [ProducesResponseType((int) HttpStatusCode.Conflict)]
    public async Task<ActionResult> RegisterPlayer(string email, string password, string name)
    {
        await _authService.CreateUser(new CreateUserArgs(email, password, name, new Roles()
        {
            Roles.Player
        }));

        return Ok();
    }

    [HttpPost(WebApi.RemoveUser)]
    [ProducesResponseType((int) HttpStatusCode.OK)]
    [ProducesResponseType((int) HttpStatusCode.NotFound)]
    public async Task<ActionResult> RemoveUser()
    {
        await _authService.RemoveUser(new RemoveUserArgs(User.Id()));
        await _playerService.RemovePlayer(new RemovePlayerArgs(User.Id()));
        return Ok();
    }

    [AllowAnonymous]
    [HttpPost(WebApi.Login)]
    [ProducesResponseType(typeof(string), (int) HttpStatusCode.OK)]
    [ProducesResponseType((int) HttpStatusCode.NotFound)]
    public async Task<ActionResult> Login(string username, string password)
    {
        var response = await _authService.GetAccessToken(new GetAccessTokenArgs(username, password));
        if (response == null)
            return Forbid();

        return Json(response);
    }
}