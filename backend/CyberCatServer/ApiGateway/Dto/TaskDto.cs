using System.Text.Json.Serialization;
using ApiGateway.Models;

namespace ApiGateway.Dto;

public class TaskDto : ITask
{
    [JsonPropertyName("name")] public string? Name { get; set; }
    [JsonPropertyName("description")] public string? Description { get; set; }

    public static TaskDto FromTask(ITask task)
    {
        return task as TaskDto ?? new TaskDto()
        {
            Name = task.Name,
            Description = task.Description
        };
    }
}