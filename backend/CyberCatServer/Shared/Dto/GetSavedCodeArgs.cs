using ProtoBuf;

namespace Shared.Dto;

[ProtoContract]
public class GetSavedCodeArgs
{
    [ProtoMember(1)] public string UserId { get; set; }
    [ProtoMember(2)] public string TaskId { get; set; }
}