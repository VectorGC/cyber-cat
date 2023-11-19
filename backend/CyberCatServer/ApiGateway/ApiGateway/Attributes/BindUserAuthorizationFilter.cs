using System.Security.Claims;
using ApiGateway.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Shared.Server.Services;

namespace ApiGateway.Attributes
{
    public class BindUserAuthorizationFilter : IAsyncAuthorizationFilter
    {
        private readonly IAuthService _authService;
        private readonly ILogger<BindUserAuthorizationFilter> _logger;

        public BindUserAuthorizationFilter(IAuthService authService, ILogger<BindUserAuthorizationFilter> logger)
        {
            _logger = logger;
            _authService = authService;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var email = context.HttpContext.User.Identity.GetEmail();
            if (string.IsNullOrEmpty(email))
            {
                return;
            }

            var response = await _authService.FindByEmail(email);
            if (!response.HasValue)
            {
                context.Result = new NotFoundResult();
                _logger.LogError("Not found user with email '{Email}'", email);
                return;
            }

            var claimsIdentity = (ClaimsIdentity) context.HttpContext.User.Identity;
            claimsIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, response.Value.Value.ToString()));
        }
    }
}