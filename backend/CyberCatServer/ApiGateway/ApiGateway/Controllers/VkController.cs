using Microsoft.AspNetCore.Mvc;
using Shared.Models.TO_REMOVE.Models.AuthorizationTokens;
using Shared.Server.Services;

namespace ApiGateway.Controllers;

[Controller]
[Route("[controller]")]
public class VkController : ControllerBase
{
    private readonly IAuthService _authService;

    public VkController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("signIn")]
    public async Task<ActionResult<VkToken>> SignIn(string email, string name, string userVkId)
    {
        var userId = await _authService.FindByEmail(new FindByEmailArgs(email));
        var password = $"{userVkId}_vk";
        if (userId != null)
        {
            await _authService.CreateUser(new CreateUserArgs(email, password, name, null));
        }

        var token = await _authService.GetAccessToken(new GetAccessTokenArgs(email, password));
        return new VkToken(token.Value);
    }
}