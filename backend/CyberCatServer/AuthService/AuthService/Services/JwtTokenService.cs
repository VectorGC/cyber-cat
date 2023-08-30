using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Shared.Server.Configurations;
using Shared.Server.Models;

namespace AuthService.Services;

public class JwtTokenService : ITokenService
{
    private const int ExpirationMinutes = 30;

    public string CreateToken(string email, string userName)
    {
        var expiration = DateTime.UtcNow.AddMinutes(ExpirationMinutes);
        var claims = CreateClaims(email, userName);
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

    private List<Claim> CreateClaims(string email, string userName)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, userName),
            new Claim(ClaimTypes.Email, email)
        };

        return claims;
    }

    private SigningCredentials CreateSigningCredentials()
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtTokenValidation.IssuerSigningKey).ToArray());
        return new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
    }
}