using System.Security.Claims;
using ApiGateway.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Shared.Server.Services;

namespace ApiGateway.Attributes;

public class BindUserAuthorizationFilter : IAsyncAuthorizationFilter
{
    private readonly IAuthGrpcService _authGrpcService;

    public BindUserAuthorizationFilter(IAuthGrpcService authGrpcService)
    {
        _authGrpcService = authGrpcService;
    }

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var email = context.HttpContext.User.Identity.GetEmail();
        if (string.IsNullOrEmpty(email))
        {
            return;
        }

        var response = await _authGrpcService.FindByEmail(email);
        if (!response.IsSucceeded)
        {
            context.Result = new ForbidResult();
            return;
        }

        var claimsIdentity = (ClaimsIdentity) context.HttpContext.User.Identity;
        claimsIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, response.UserId.Value.ToString()));
    }
}