using ProtoBuf;

namespace Shared.Models.Domain.Users
{
    [ProtoContract]
    public class UserModel
    {
        [ProtoMember(1)] public string Email { get; set; }
        [ProtoMember(2)] public string FirstName { get; set; }
        [ProtoMember(3)] public Roles Roles { get; set; }
    }
}