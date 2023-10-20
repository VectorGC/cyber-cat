using Microsoft.AspNetCore.Mvc;
using Shared.Server.Services;

namespace ApiGateway.Controllers;

[Controller]
[Route("[controller]")]
public class OAuthController : ControllerBase
{
    private readonly IAuthGrpcService _authGrpcService;

    public OAuthController(IAuthGrpcService authGrpcService)
    {
        _authGrpcService = authGrpcService;
    }

    [HttpPost("signIn")]
    public async Task<ActionResult<string>> SignIn(string email, string name)
    {
        var response = await _authGrpcService.SignInWithVk(new OAuthSignIn(email, name));
        return response;
    }
}