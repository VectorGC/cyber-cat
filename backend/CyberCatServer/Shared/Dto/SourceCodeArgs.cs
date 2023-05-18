using ProtoBuf;

namespace Shared.Dto;

[ProtoContract]
public class SourceCodeArgs
{
    [ProtoMember(1)] public string SourceCode { get; set; }
}