using System.Text.Json.Serialization;
using ProtoBuf;

namespace Shared.Dto;

[ProtoContract]
public class TaskDto
{
    [ProtoMember(1)]
    [JsonPropertyName("name")]
    public string Name { get; init; }

    [ProtoMember(2)]
    [JsonPropertyName("description")]
    public string Description { get; init; }
}