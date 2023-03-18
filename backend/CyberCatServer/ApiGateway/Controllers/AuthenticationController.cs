using System.Net;
using ApiGateway.Exceptions;
using ApiGateway.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiGateway.Controllers;

[Controller]
[Route("[controller]")]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthUserService _authUserService;

    public AuthenticationController(IAuthUserService authUserService)
    {
        _authUserService = authUserService;
    }

    /// <summary>
    /// Выдача токена по email и паролю.
    /// </summary>
    /// <returns>Токен</returns>
    [HttpGet("login")]
    [ProducesResponseType(typeof(string), (int) HttpStatusCode.OK)]
    [ProducesResponseType((int) HttpStatusCode.Forbidden)]
    [ProducesResponseType((int) HttpStatusCode.UnprocessableEntity)]
    public async Task<IActionResult> Login(string email, string password)
    {
        try
        {
            var token = await _authUserService.Authenticate(email, password);
            return Ok(token);
        }
        catch (UserNotFound notFound)
        {
            return Forbid(notFound.Message);
        }
        catch (UnprocessableTokenException tokenException)
        {
            return UnprocessableEntity(tokenException);
        }
        catch (UnauthorizedAccessException exception)
        {
            return Unauthorized(exception);
        }
    }
}