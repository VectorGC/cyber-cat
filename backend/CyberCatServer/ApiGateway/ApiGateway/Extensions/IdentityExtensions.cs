using System.Security.Authentication;
using System.Security.Claims;
using System.Security.Principal;
using Microsoft.AspNetCore.Authorization;
using Shared.Server.Models;
using Shared.Server.Services;

namespace ApiGateway.Extensions;

public static class IdentityExtensions
{
    public static string GetEmail(this IIdentity identity)
    {
        if (identity == null)
            throw new ArgumentNullException(nameof(identity));
        return identity is ClaimsIdentity identity1 ? identity1.FindFirstValue(ClaimTypes.Email) : string.Empty;
    }

    public static string FindFirstValue(this ClaimsIdentity identity, string claimType)
    {
        if (identity == null)
            throw new ArgumentNullException(nameof(identity));
        return identity.FindFirst(claimType)?.Value;
    }
}