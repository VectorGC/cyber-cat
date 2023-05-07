using ApiGateway.Models;
using ApiGateway.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Shared.Exceptions;

namespace ApiGateway.Authorization;

public class AuthorizeTokenGuard : IAsyncAuthorizationFilter
{
    private readonly IAuthUserService _authUserService;
    private readonly ILogger _logger;

    public AuthorizeTokenGuard(IAuthUserService authUserService, ILogger<AuthorizeTokenGuard> logger)
    {
        _authUserService = authUserService;
        _logger = logger;
    }

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var token = context.HttpContext.Request.Query["token"];
        try
        {
            if (string.IsNullOrEmpty(token))
            {
                context.Result = new UnauthorizedObjectResult("token not found");
                return;
            }

            var userId = await _authUserService.Authorize(token);
            context.HttpContext.Items[typeof(UserId)] = userId;
        }
        catch (UserNotFound)
        {
            _logger.LogInformation("token '{Token}' is invalid", token);
            context.Result = new UnauthorizedObjectResult("token is invalid");
        }
    }
}