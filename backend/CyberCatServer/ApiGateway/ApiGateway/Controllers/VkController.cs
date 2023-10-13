using Microsoft.AspNetCore.Mvc;
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
    public async Task<ActionResult> SignIn(string email, string name)
    {
        var response = await _authGrpcService.SignInWithVk(new OAuthSignIn(email, name));
        return response;
    }
}