using System.Text.Json.Serialization;
using ProtoBuf;
using Shared.Models;

namespace Shared.Dto;

[ProtoContract]
public class TaskDto
{
    [JsonPropertyName("name")]
    [ProtoMember(1)]
    public string Name { get; set; }

    [JsonPropertyName("description")]
    [ProtoMember(2)]
    public string Description { get; set; }
}

public static class TaskDtoExtensions
{
    public static TaskDto ToDto(this ITask task)
    {
        return new TaskDto
        {
            Name = task.Name,
            Description = task.Description
        };
    }
}