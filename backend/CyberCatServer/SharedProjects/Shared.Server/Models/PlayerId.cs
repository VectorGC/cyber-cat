using ProtoBuf;

namespace Shared.Server.Models;

[ProtoContract]
public class PlayerId
{
    [ProtoMember(1)] public long Value { get; set; }

    public PlayerId(string id)
    {
        Value = long.Parse(id);
    }

    public PlayerId(long id)
    {
        Value = id;
    }

    public PlayerId()
    {
    }

    public override string ToString()
    {
        return Value.ToString();
    }
}