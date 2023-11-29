using ProtoBuf;

namespace Shared.Models.Domain.Users
{
    [ProtoContract(SkipConstructor = true)]
    public class Role
    {
        [ProtoMember(1)] public string Id { get; }

        public Role(string id)
        {
            Id = id;
        }
    }
}