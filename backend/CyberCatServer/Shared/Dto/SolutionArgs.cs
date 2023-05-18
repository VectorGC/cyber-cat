using ProtoBuf;

namespace Shared.Dto;

[ProtoContract]
public class SolutionArgs
{
    [ProtoMember(1)] public string UserId { get; set; }
    [ProtoMember(2)] public string TaskId { get; set; }
    [ProtoMember(3)] public string SolutionCode { get; set; }
}