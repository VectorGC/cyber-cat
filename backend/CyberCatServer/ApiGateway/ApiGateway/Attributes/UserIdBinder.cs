using System.Security.Claims;
using ApiGateway.Extensions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Shared.Server.Models;

namespace ApiGateway.Attributes
{
    public class UserIdBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext.ModelType != typeof(UserId))
            {
                return Task.CompletedTask;
            }

            var userId = GetUserId(bindingContext.HttpContext.User);
            if (userId == null)
            {
                bindingContext.ModelState.AddModelError(bindingContext.ModelName, "Invalid value");
                bindingContext.Result = ModelBindingResult.Failed();
                return Task.CompletedTask;
            }

            bindingContext.Result = ModelBindingResult.Success(userId);

            return Task.CompletedTask;
        }

        public static UserId GetUserId(ClaimsPrincipal claimsPrincipal)
        {
            var identity = claimsPrincipal.Identity;
            if (identity == null)
                throw new ArgumentNullException(nameof(identity));
            var userIdentifier = identity is ClaimsIdentity identity1 ? identity1.FindFirstValue(ClaimTypes.NameIdentifier) : string.Empty;
            return new UserId(userIdentifier);
        }
    }
}