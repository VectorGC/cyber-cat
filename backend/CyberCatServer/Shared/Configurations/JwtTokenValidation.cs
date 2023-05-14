using System.Text.Json;
using Microsoft.IdentityModel.Tokens;

namespace Shared.Configurations;

public static class JwtTokenValidation
{
    public const string Issuer = "AuthService";
    public const string Audience = "AuthService";
    public static readonly SymmetricSecurityKey IssuerSigningKey = new("!CyberCatSecret!"u8.ToArray());

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

public class AuthServiceConnectionStrings
{
    public const string AuthServiceGrpcSection = "ConnectionStrings";

    public string AuthServiceGrpcEndpoint { get; set; }
}