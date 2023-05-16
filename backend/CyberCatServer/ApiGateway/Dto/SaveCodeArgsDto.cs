using System.Text.Json.Serialization;

namespace ApiGateway.Dto;

public class SaveCodeArgsDto
{
    [JsonPropertyName("task_id")] public string TaskId { get; set; } = null!;
    [JsonPropertyName("source_code")] public string SourceCode { get; set; } = null!;
}