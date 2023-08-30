using ProtoBuf;

namespace Shared.Server.Dto.Args;

[ProtoContract]
public class CreateUserArgs
{
    [ProtoMember(1)] public string Email { get; set; }
    [ProtoMember(2)] public string Password { get; set; }
    [ProtoMember(3)] public string Name { get; set; }
}