using ProtoBuf;

namespace Shared.Models.Dto.Args
{
    [ProtoContract]
    public class CreateUserArgs
    {
        [ProtoMember(1)] public UserDto User { get; set; }
        [ProtoMember(2)] public string Password { get; set; }
    }
}