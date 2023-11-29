using Microsoft.AspNetCore.Mvc;
using Shared.Server.Services;

namespace ApiGateway.Controllers;

[Controller]
[Route("[controller]")]
public class DevController : ControllerBase
{
    private readonly IAuthService _authService;

    public DevController(IAuthService authService)
    {
        _authService = authService;
    }
}