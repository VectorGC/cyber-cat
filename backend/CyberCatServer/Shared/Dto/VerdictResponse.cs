using ProtoBuf;
using Shared.Models;

namespace Shared.Dto;

[ProtoContract]
public class VerdictResponse
{
    [ProtoMember(1)] public VerdictStatus Status { get; set; }
    [ProtoMember(2)] public string Error { get; set; }
    [ProtoMember(3)] public int TestsPassed { get; set; }
}