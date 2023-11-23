using System.Net;
using ApiGateway.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Server.Ids;
using Shared.Server.Infrastructure;
using Shared.Server.Services;

namespace ApiGateway.Controllers;

[Controller]
[Route("[controller]")]
[Authorize]
public class AuthController : Controller
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
    public async Task<JsonResult> SignIn(string username, string password)
    {
        var response = await _authService.GetAccessToken(new GetAccessTokenArgs(username, password));
        return Json(response.Value);
    }

    [HttpPost("remove")]
    [ProducesResponseType((int) HttpStatusCode.OK)]
    public async Task<ActionResult> Remove(string password)
    {
        return await _authService.Remove(new RemoveArgs(User.Id(), password));
    }
}