using ProtoBuf;

namespace Shared.Dto.Args;

[ProtoContract]
public class CreateUserArgs
{
    [ProtoMember(1)] public UserDto User { get; set; }
    [ProtoMember(2)] public string Password { get; set; }
}