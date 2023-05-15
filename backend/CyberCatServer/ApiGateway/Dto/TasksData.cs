using System.Text.Json.Serialization;

namespace ApiGateway.Dto;

public class TasksData
{
    [JsonPropertyName("tasks")] public Dictionary<string, TaskDto> Tasks { get; set; } = new Dictionary<string, TaskDto>();
}