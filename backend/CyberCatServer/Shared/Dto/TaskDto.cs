using System.Text.Json.Serialization;
using ProtoBuf;
using Shared.Models;

namespace Shared.Dto;

[ProtoContract]
public class TaskDto : ITask
{
    [ProtoMember(1)]
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [ProtoMember(2)]
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    public static TaskDto FromTask(ITask task)
    {
        return task as TaskDto ?? new TaskDto()
        {
            Name = task.Name,
            Description = task.Description
        };
    }
}