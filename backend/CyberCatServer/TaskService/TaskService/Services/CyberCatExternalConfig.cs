using System.Text.Json.Serialization;

namespace TaskService.Services;

public class CyberCatExternalConfig
{
    [JsonPropertyName("shared_tasks_webhook")]
    public string SharedTasksWebHook { get; set; }
}