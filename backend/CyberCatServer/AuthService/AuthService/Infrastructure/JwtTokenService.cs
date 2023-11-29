using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using AuthService.Domain;
using AuthService.Domain.Models;
using Microsoft.IdentityModel.Tokens;
using Shared.Models.Infrastructure.Authorization;
using Shared.Server.Configurations;

namespace AuthService.Infrastructure;

public class JwtTokenService : ITokenService
{
    private const int ExpirationMinutes = 30;

    public AuthorizationToken CreateToken(UserEntity user)
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

    private List<Claim> CreateClaims(UserEntity user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Email, user.Email)
        };

        var rolesClaims = user.Roles.Select(role => new Claim(ClaimTypes.Role, role));
        claims.AddRange(rolesClaims);

        return claims;
    }

    private SigningCredentials CreateSigningCredentials()
    {
        return new SigningCredentials(JwtTokenValidation.TokenValidationParameters.IssuerSigningKey, SecurityAlgorithms.HmacSha256);
    }
}