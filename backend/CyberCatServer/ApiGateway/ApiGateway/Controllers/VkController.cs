using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Models.Domain.Users;
using Shared.Models.Infrastructure;
using Shared.Server.Application.Services;

namespace ApiGateway.Controllers;

[Controller]
[Authorize]
public class VkController : Controller
{
    private readonly IAuthService _authService;
    private readonly IPlayerService _playerService;

    public VkController(IAuthService authService, IPlayerService playerService)
    {
        _playerService = playerService;
        _authService = authService;
    }

    [AllowAnonymous]
    [HttpPost(WebApi.LoginWithVk)]
    [ProducesResponseType(typeof(string), (int) HttpStatusCode.OK)]
    [ProducesResponseType((int) HttpStatusCode.NotFound)]
    public async Task<ActionResult> Login(string email, string userName, string vkId)
    {
        var token = await _authService.GetAccessTokenWithVk(new GetAccessTokenWithVkArgs(email, userName, vkId, new Roles()
        {
            Roles.Player
        }));

        if (token == null)
            return Forbid();

        return Json(token);
    }
}