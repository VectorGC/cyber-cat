using System.Security.Claims;
using ApiGateway.Extensions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Shared.Server.Models;

namespace ApiGateway.Attributes;

public class UserIdBinder : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        if (bindingContext.ModelType != typeof(UserId))
        {
            return Task.CompletedTask;
        }

        var userIdentifier = GetUserIdentifier(bindingContext.HttpContext.User);
        if (string.IsNullOrEmpty(userIdentifier))
        {
            bindingContext.ModelState.AddModelError(bindingContext.ModelName, "Invalid value");
            bindingContext.Result = ModelBindingResult.Failed();
            return Task.CompletedTask;
        }

        var userId = new UserId(userIdentifier);
        bindingContext.Result = ModelBindingResult.Success(userId);

        return Task.CompletedTask;
    }

    private string GetUserIdentifier(ClaimsPrincipal claimsPrincipal)
    {
        var identity = claimsPrincipal.Identity;
        if (identity == null)
            throw new ArgumentNullException(nameof(identity));
        return identity is ClaimsIdentity identity1 ? identity1.FindFirstValue(ClaimTypes.NameIdentifier) : string.Empty;
    }
}