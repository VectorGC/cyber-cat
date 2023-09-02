using ProtoBuf;

namespace Shared.Server.Models;

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
}