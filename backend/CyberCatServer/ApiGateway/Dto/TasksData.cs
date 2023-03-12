using System.Text.Json.Serialization;

namespace ApiGateway.Dto;

public class TasksData
{
    [JsonPropertyName("tasks")] public Dictionary<string, TaskData> Tasks { get; set; } = new Dictionary<string, TaskData>();
}