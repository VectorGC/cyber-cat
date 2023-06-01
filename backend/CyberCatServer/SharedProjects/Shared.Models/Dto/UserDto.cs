using ProtoBuf;
using Shared.Models;

namespace Shared.Dto
{
    [ProtoContract]
    public class UserDto : IUser
    {
        [ProtoMember(1)] public string UserName { get; set; }
        [ProtoMember(2)] public string Email { get; set; }
    }
}