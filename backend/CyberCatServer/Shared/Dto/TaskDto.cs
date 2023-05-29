using System.Text.Json.Serialization;
using ProtoBuf;
using Shared.Models;

namespace Shared.Dto;

[ProtoContract]
public class TaskDto : ITask
{
    [JsonPropertyName("name")]
    [ProtoMember(1)]
    public string Name { get; set; }

    [JsonPropertyName("description")]
    [ProtoMember(2)]
    public string Description { get; set; }

    public TaskDto(ITask task)
    {
        Name = task.Name;
        Description = task.Description;
    }

    public TaskDto()
    {
    }
}