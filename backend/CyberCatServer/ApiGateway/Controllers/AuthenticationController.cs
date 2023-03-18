using System.Net;
using ApiGateway.Exceptions;
using ApiGateway.Repositories;
using ApiGateway.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiGateway.Controllers;

[Controller]
[Route("[controller]")]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthUserService _authUserService;
    private readonly IUserRepository _userRepository;

    public AuthenticationController(IAuthUserService authUserService, IUserRepository userRepository)
    {
        _authUserService = authUserService;
        _userRepository = userRepository;
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
            var tokenUser = await _authUserService.Authenticate(email, password);
            var userId = await _authUserService.Authorize(tokenUser);
            var user = await _userRepository.GetUser(userId);

            return Ok(new
            {
                token = tokenUser,
                name = user.Name
            });
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