using System.Text.Json.Serialization;

namespace ApiGateway.Dto;

public class VerdictResult
{
    [JsonPropertyName("error")] public int Error { get; set; }
    [JsonPropertyName("error_data")] public VerdictError? ErrorData { get; set; } = null!;
}