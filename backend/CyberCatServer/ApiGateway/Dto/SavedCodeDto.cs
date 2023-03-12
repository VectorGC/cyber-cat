using System.Text.Json.Serialization;

namespace ApiGateway.Dto;

public class SavedCodeDto
{
    [JsonPropertyName("text")] public string Text { get; set; } = null!;
}