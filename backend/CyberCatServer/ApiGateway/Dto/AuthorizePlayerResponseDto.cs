using System.Text.Json.Serialization;

namespace ApiGateway.Dto;

public class AuthorizePlayerResponseDto
{
    [JsonPropertyName("name")] public string Name { get; set; }
}