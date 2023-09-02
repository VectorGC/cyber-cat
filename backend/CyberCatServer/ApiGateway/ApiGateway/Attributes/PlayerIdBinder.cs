using System.Security.Claims;
using ApiGateway.Extensions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Shared.Server.Ids;

namespace ApiGateway.Attributes;

public class PlayerIdBinder : IModelBinder
{
    public const string ClaimType = "player";
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        if (bindingContext.ModelType != typeof(PlayerId))
        {
            return Task.CompletedTask;
        }

        var playerIdentifier = GetPlayerIdentifier(bindingContext.HttpContext.User);
        if (string.IsNullOrEmpty(playerIdentifier))
        {
            bindingContext.ModelState.AddModelError(bindingContext.ModelName, "Invalid value");
            bindingContext.Result = ModelBindingResult.Failed();
            return Task.CompletedTask;
        }

        var playerId = new PlayerId(playerIdentifier);
        bindingContext.Result = ModelBindingResult.Success(playerId);

        return Task.CompletedTask;
    }

    private string GetPlayerIdentifier(ClaimsPrincipal claimsPrincipal)
    {
        var identity = claimsPrincipal.Identity;
        if (identity == null)
            throw new ArgumentNullException(nameof(identity));
        return identity is ClaimsIdentity identity1 ? identity1.FindFirstValue(ClaimType) : string.Empty;
    }
}