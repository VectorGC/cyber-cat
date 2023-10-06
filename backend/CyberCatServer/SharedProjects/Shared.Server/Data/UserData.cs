using ProtoBuf;
using Shared.Server.Ids;

namespace Shared.Server.Data
{
    [ProtoContract]
    public class UserDto
    {
        [ProtoMember(1)] public UserId Id { get; set; }
        [ProtoMember(2)] public string UserName { get; set; }
        [ProtoMember(3)] public string Email { get; set; }
    }
}