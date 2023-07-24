using ProtoBuf;

namespace Shared.Models.Dto.Args
{
    [ProtoContract]
    public class PlayerAddBtcArgs
    {
        [ProtoMember(1)] public long PlayerId { get; set; }
        [ProtoMember(2)] public int BitcoinsCount { get; set; }
    }
}