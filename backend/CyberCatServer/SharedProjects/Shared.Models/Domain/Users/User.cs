using ProtoBuf;

namespace Shared.Models.Domain.Users
{
    public class User
    {
        [ProtoMember(1)] public UserId Id { get; set; }
        [ProtoMember(2)] public string UserName { get; set; }
        [ProtoMember(3)] public string Email { get; set; }
        [ProtoMember(4)] public Role Role { get; set; }
    }
}