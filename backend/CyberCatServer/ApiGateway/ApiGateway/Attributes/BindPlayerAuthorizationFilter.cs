using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Shared.Server.Ids;
using Shared.Server.Services;

namespace ApiGateway.Attributes;

public class BindPlayerAuthorizationFilter : IAsyncAuthorizationFilter
{
    private readonly IPlayerGrpcService _playerGrpcService;
    private readonly ILogger<BindPlayerAuthorizationFilter> _logger;

    public BindPlayerAuthorizationFilter(IPlayerGrpcService playerGrpcService, ILogger<BindPlayerAuthorizationFilter> logger)
    {
        _logger = logger;
        _playerGrpcService = playerGrpcService;
    }

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var playerIdParameter = context.ActionDescriptor.Parameters
            .OfType<ControllerParameterDescriptor>()
            .FirstOrDefault(p => p.ParameterType == typeof(PlayerId) && p.ParameterInfo.GetCustomAttributes(typeof(FromPlayerAttribute), false).Any());

        if (playerIdParameter == null)
        {
            return;
        }

        var userId = UserIdBinder.GetUserId(context.HttpContext.User);
        if (userId == null)
        {
            return;
        }

        var response = await _playerGrpcService.GetPlayerByUserId(userId);
        if (!response.HasValue)
        {
            context.Result = new NotFoundResult();
            _logger.LogError("Not found player for user '{UserId}'", userId);
            return;
        }

        var claimsIdentity = (ClaimsIdentity) context.HttpContext.User.Identity;
        claimsIdentity.AddClaim(new Claim(PlayerIdBinder.ClaimType, response.Value.Value.ToString()));
    }
}