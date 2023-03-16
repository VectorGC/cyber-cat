using ApiGateway.Exceptions;
using ApiGateway.Models;
using ApiGateway.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ApiGateway.Authorization;

public class AuthorizeRequireToken : ControllerBase, IAsyncAuthorizationFilter
{
    private readonly IAuthenticationUserService _authenticationUserService;

    public AuthorizeRequireToken(IAuthenticationUserService authenticationUserService)
    {
        _authenticationUserService = authenticationUserService;
    }

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        try
        {
            var token = context.HttpContext.Request.Query["token"];
            var user = await _authenticationUserService.Authorize(token);
            context.HttpContext.Items[typeof(IUser)] = user;
        }
        catch (UserNotFound)
        {
            context.Result = new UnauthorizedObjectResult("token is invalid");
        }
        catch (ArgumentNullException)
        {
            context.Result = new UnauthorizedObjectResult("token not found");
        }
    }
}