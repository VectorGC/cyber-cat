using System.Text.Json.Serialization;
using ProtoBuf;
using Shared.Models;

namespace Shared.Dto;

[ProtoContract]
public class TaskResponse : ITask
{
    [ProtoMember(1)]
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [ProtoMember(2)]
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    public static TaskResponse FromTask(ITask task)
    {
        return task as TaskResponse ?? new TaskResponse()
        {
            Name = task.Name,
            Description = task.Description
        };
    }
}