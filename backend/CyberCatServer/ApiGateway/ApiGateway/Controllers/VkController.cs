using Microsoft.AspNetCore.Mvc;
using Shared.Models.Models.AuthorizationTokens;
using Shared.Server.Services;

namespace ApiGateway.Controllers;

[Controller]
[Route("[controller]")]
public class VkController : ControllerBase
{
    private readonly IAuthGrpcService _authGrpcService;

    public VkController(IAuthGrpcService authGrpcService)
    {
        _authGrpcService = authGrpcService;
    }

    [HttpPost("signIn")]
    public async Task<ActionResult<VkToken>> SignIn(string email, string name, string userVkId)
    {
        var userId = await _authGrpcService.FindByEmail(email);
        var password = $"{userVkId}_vk";
        if (!userId.HasValue)
        {
            await _authGrpcService.CreateUser(new CreateUserArgs(email, password, name));
        }

        var token = await _authGrpcService.GetAccessToken(new GetAccessTokenArgs(email, password));
        return new VkToken()
        {
            Value = token
        };
    }
}