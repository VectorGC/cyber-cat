using Microsoft.AspNetCore.Mvc;
using Shared.Models.Models.AuthorizationTokens;
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
        var userId = await _authService.FindByEmail(email);
        var password = $"{userVkId}_vk";
        if (!userId.HasValue)
        {
            await _authService.CreateUser(new CreateUserArgs(email, password, name));
        }

        var token = await _authService.GetAccessToken(new GetAccessTokenArgs(email, password));
        return new VkToken(token.Value.Value);
    }
}