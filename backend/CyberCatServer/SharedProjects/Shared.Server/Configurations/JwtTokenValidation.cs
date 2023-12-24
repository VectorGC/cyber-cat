using System.Linq;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Shared.Server.Configurations;

public static class JwtTokenValidation
{
    public static TokenValidationParameters TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "AuthService",
        ValidAudience = "AuthService",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("!CyberCatSecret!").ToArray())
    };
}