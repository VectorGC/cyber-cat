using ProtoBuf;

namespace Shared.Models.Domain.Users
{
    [ProtoContract]
    public class UserId
    {
        [ProtoMember(1)] public long Value { get; set; }

        public UserId(string id)
        {
            Value = long.Parse(id);
        }

        public UserId(long id)
        {
            Value = id;
        }

        public UserId()
        {
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        public static implicit operator UserId(long value) => new UserId(value);
        public static implicit operator long(UserId userId) => userId.Value;
    }
}