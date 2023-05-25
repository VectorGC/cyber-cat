using System.Text.Json.Serialization;

namespace ApiGateway.Dto;

public class TaskDto
{
    [JsonPropertyName("name")] public string Name { get; set; }
    [JsonPropertyName("description")] public string Description { get; set; }
}