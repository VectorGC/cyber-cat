using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ApiGateway.Extensions;

public static class SwaggerGetOptionsAuthExtensions
{
    public static void AddJwtSecurityDefinition(this SwaggerGenOptions options)
    {
        /*
        options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "Please enter a valid token",
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            BearerFormat = JwtConstants.TokenType,
            Scheme = JwtBearerDefaults.AuthenticationScheme
        });*/
        // Делаем в сваггере удобный виджет, чтобы авторизоваться по логину и паролю, а не по JWT токену.
        // https://stackoverflow.com/questions/38784537/use-jwt-authorization-bearer-in-swagger-in-asp-net-core/47709074#47709074
        // https://github.com/domaindrivendev/Swashbuckle.AspNetCore/issues/1257
        options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme()
        {
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.OAuth2,
            BearerFormat = JwtConstants.TokenType,
            Scheme = JwtBearerDefaults.AuthenticationScheme,
            Flows = new OpenApiOAuthFlows
            {
                Password = new OpenApiOAuthFlow
                {
                    TokenUrl = new Uri("auth/login", UriKind.Relative)
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
                        Type = ReferenceType.SecurityScheme,
                        Id = JwtBearerDefaults.AuthenticationScheme
                        //Id = "oauth2"
                    }
                },
                new string[] { }
            }
        });
    }
}