using ProtoBuf;

namespace Shared.Dto;

[ProtoContract]
public class TaskIdArg
{
    [ProtoMember(1)] public string Id { get; set; }
}