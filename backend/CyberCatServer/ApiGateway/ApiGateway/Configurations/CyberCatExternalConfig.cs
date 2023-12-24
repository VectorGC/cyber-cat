using System.Text.Json.Serialization;

namespace ApiGateway.Configurations;

public class CyberCatExternalConfig
{
    [JsonPropertyName("shared_tasks_webhook")]
    public string SharedTasksWebHook { get; set; }
}