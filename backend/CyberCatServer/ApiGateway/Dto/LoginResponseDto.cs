using System.Text.Json.Serialization;

namespace ApiGateway.Dto;

public class LoginResponseDto
{
    [JsonPropertyName("access_token")] public string AccessToken { get; set; }
}