using Microsoft.AspNetCore.Identity;

namespace AuthService.Configurations;

public static class AuthIdentity
{
    public static void IdentityOptions(IdentityOptions options)
    {
        options.Password.RequireDigit = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireLowercase = true;
        options.Password.RequiredLength = 3;
    }
}