using System.Net.Http.Headers;
using System.Net.Http.Json;
using ApiGateway.Dto;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace ApiGatewayEnd2EndTests.Extensions;

public static class AuthHttpClientExtensions
{
    public static async Task AddJwtAuthorizationHeaderAsync(this HttpClient client, string username, string password)
    {
        var token = await GetToken(client, username, password);
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, token);
    }

    public static async Task<string> GetToken(HttpClient client, string username, string password)
    {
        var form = new MultipartFormDataContent();
        form.Add(new StringContent(username), "username");
        form.Add(new StringContent(password), "password");

        var response = await client.PostAsync("http://localhost:5000/auth/login", form);
        var token = await response.Content.ReadFromJsonAsync<TokenResponseDto>();

        return token.AccessToken;
    }
}