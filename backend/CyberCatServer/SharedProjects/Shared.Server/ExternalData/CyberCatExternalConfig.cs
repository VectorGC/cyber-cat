using System.Text.Json.Serialization;

namespace Shared.Server.ExternalData;

public class CyberCatExternalConfig
{
    [JsonPropertyName("shared_tasks_webhook")]
    public string SharedTasksWebHook { get; set; }
}