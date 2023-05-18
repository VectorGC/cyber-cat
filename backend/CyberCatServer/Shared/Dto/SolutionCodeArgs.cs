using ProtoBuf;

namespace Shared.Dto;

[ProtoContract]
public class SolutionCodeArgs
{
    [ProtoMember(1)] public string SourceCode { get; set; }
}