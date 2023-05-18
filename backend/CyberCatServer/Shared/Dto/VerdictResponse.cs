using ProtoBuf;

namespace Shared.Dto;

[ProtoContract]
public class VerdictResponse
{
    [ProtoMember(1)] public VerdictStatus Status { get; set; }
}