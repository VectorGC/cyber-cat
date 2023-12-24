using System;
using System.Security.Claims;
using Shared.Models.Domain.Users;

namespace Shared.Server.Infrastructure;

public static class UserExtensions
{
    public static UserId Id(this ClaimsPrincipal user)
    {
        if (user.Identity == null)
            throw new ArgumentNullException(nameof(user.Identity));
        var identifier = user.Identity is ClaimsIdentity identity1 ? identity1.FindFirstValue(ClaimTypes.NameIdentifier) : string.Empty;
        var id = long.Parse(identifier);

        return new UserId(id);
    }

    private static string FindFirstValue(this ClaimsIdentity identity, string claimType)
    {
        if (identity == null)
            throw new ArgumentNullException(nameof(identity));
        return identity.FindFirst(claimType)?.Value;
    }
}