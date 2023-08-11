using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace AuthService.JwtValidation;

public static class JwtTokenValidation
{
    public const string Issuer = "AuthService";
    public const string Audience = "AuthService";
    public static readonly SymmetricSecurityKey IssuerSigningKey = new(Encoding.UTF8.GetBytes("!CyberCatSecret!").ToArray());

    public static TokenValidationParameters CreateTokenParameters()
    {
        return new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = Issuer,
            ValidAudience = Audience,
            IssuerSigningKey = IssuerSigningKey
        };
    }
}

public static class AuthIdentity
{
    public static void SetServiceOptions(IdentityOptions options)
    {
        options.Password.RequireDigit = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireLowercase = true;
        options.Password.RequiredLength = 3;
    }
}