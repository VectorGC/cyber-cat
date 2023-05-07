using ProtoBuf;

namespace Shared.Dto;

[ProtoContract]
public class AccessTokenDto
{
    [ProtoMember(1)] public string Value { get; set; }
}