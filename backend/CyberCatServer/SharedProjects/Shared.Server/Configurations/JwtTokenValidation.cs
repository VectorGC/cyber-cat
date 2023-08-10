namespace AuthService.JwtValidation;

public static class JwtTokenValidation
{
    public const string Issuer = "AuthService";
    public const string Audience = "AuthService";
    public const string IssuerSigningKey = "!CyberCatSecret!";
}