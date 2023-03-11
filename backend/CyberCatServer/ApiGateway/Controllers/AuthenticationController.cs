using Microsoft.AspNetCore.Mvc;

namespace ApiGateway.Controllers;

[Controller]
[Route("[controller]")]
public class AuthenticationController : ControllerBase
{
    public const string TOKEN = "test_token";

    [HttpGet]
    public IActionResult Login(string email, string password)
    {
        return Ok(TOKEN);
    }
}