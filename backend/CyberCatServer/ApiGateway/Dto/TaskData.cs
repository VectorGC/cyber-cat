using System.Text.Json.Serialization;

namespace ApiGateway.Dto;

public class TaskData
{
    [JsonPropertyName("id")] public int Id { get; set; }
    [JsonPropertyName("name")] public string? Name { get; set; }
    [JsonPropertyName("desc")] public string? Description { get; set; }
    [JsonPropertyName("output")] public string? Output { get; set; }
    [JsonPropertyName("score")] public float TotalScore { get; set; }

    [JsonPropertyName("completion")] public float Completion { get; set; }
}