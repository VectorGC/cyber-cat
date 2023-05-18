using System.Text.Json.Serialization;

namespace Shared.Dto;

public class AuthorizePlayerResponse
{
    [JsonPropertyName("name")] public string Name { get; set; }
}