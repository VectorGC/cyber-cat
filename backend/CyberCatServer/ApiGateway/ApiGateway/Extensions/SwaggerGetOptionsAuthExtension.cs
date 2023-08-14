using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ApiGateway.Extensions;

public static class SwaggerGetOptionsAuthExtensions
{
    public static void AddJwtSecurityDefinition(this SwaggerGenOptions options, string host)
    {
        if (string.IsNullOrEmpty(host))
        {
            throw new ArgumentNullException(nameof(host));
        }

        // https://stackoverflow.com/questions/38784537/use-jwt-authorization-bearer-in-swagger-in-asp-net-core/47709074#47709074
        // https://github.com/domaindrivendev/Swashbuckle.AspNetCore/issues/1257
        options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.OAuth2,
            BearerFormat = JwtConstants.TokenType,
            Scheme = JwtBearerDefaults.AuthenticationScheme,
            Flows = new OpenApiOAuthFlows
            {
                Password = new OpenApiOAuthFlow
                {
                    TokenUrl = new Uri($"{host}/auth/login")
                }
            }
        });

        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                },
                new string[] { }
            }
        });
    }
}