using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AuthService.JwtValidation;
using Microsoft.IdentityModel.Tokens;
using Shared.Models;
using Shared.Models.Models;

namespace AuthService.Services;

public class JwtTokenService : ITokenService
{
    private const int ExpirationMinutes = 30;

    public string CreateToken(IUser user)
    {
        var expiration = DateTime.UtcNow.AddMinutes(ExpirationMinutes);
        var claims = CreateClaims(user);
        var signinCredentials = CreateSigningCredentials();
        var token = CreateJwtToken(claims, signinCredentials, expiration);

        var tokenHandler = new JwtSecurityTokenHandler();
        return tokenHandler.WriteToken(token);
    }

    private JwtSecurityToken CreateJwtToken(List<Claim> claims, SigningCredentials credentials, DateTime expiration)
    {
        return new JwtSecurityToken(
            JwtTokenValidation.Issuer,
            JwtTokenValidation.Audience,
            claims,
            expires: expiration,
            signingCredentials: credentials
        );
    }

    private List<Claim> CreateClaims(IUser user)
    {
        try
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Email),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email)
            };

            return claims;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private SigningCredentials CreateSigningCredentials()
    {
        return new SigningCredentials(JwtTokenValidation.IssuerSigningKey, SecurityAlgorithms.HmacSha256);
    }
}