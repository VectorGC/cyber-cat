using ProtoBuf;

namespace Shared.Dto;

[ProtoContract]
public class GetTaskResponse
{
    [ProtoMember(1)]
    public string Name { get; init; }

    [ProtoMember(2)]
    public string Description { get; init; }
}