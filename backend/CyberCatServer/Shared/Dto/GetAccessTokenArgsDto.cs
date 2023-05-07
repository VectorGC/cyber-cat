using ProtoBuf;

namespace Shared.Dto;

[ProtoContract]
public class GetAccessTokenArgsDto
{
    [ProtoMember(1)] public string Email { get; set; }
    [ProtoMember(2)] public string Password { get; set; }
}