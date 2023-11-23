using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Shared.Models.Domain.Users;
using Shared.Models.Infrastructure;
using Shared.Models.Infrastructure.Authorization;
using Shared.Models.Models.AuthorizationTokens;
using Shared.Server.Configurations;
using Shared.Server.Data;

namespace AuthService.Services;

public class JwtTokenService : ITokenService
{
    private const int ExpirationMinutes = 30;

    public AuthorizationToken CreateToken(User user)
    {
        var expiration = DateTime.UtcNow.AddMinutes(ExpirationMinutes);
        var claims = CreateClaims(user);
        var signinCredentials = CreateSigningCredentials();

        var jwtSecurityToken = CreateJwtToken(claims, signinCredentials, expiration);
        var tokenHandler = new JwtSecurityTokenHandler();

        var token = tokenHandler.WriteToken(jwtSecurityToken);

        return new JwtAccessToken(token, user.UserName);
    }

    private JwtSecurityToken CreateJwtToken(List<Claim> claims, SigningCredentials credentials, DateTime expiration)
    {
        return new JwtSecurityToken(
            JwtTokenValidation.TokenValidationParameters.ValidIssuer,
            JwtTokenValidation.TokenValidationParameters.ValidAudience,
            claims,
            expires: expiration,
            signingCredentials: credentials
        );
    }

    private List<Claim> CreateClaims(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, "Admin")
        };

        return claims;
    }

    private SigningCredentials CreateSigningCredentials()
    {
        return new SigningCredentials(JwtTokenValidation.TokenValidationParameters.IssuerSigningKey, SecurityAlgorithms.HmacSha256);
    }
}