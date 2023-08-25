using ProtoBuf;

namespace Shared.Models.Dto.Args
{
    [ProtoContract]
    public class PlayerBtcArgs
    {
        [ProtoMember(1)] public string PlayerId { get; set; }
        [ProtoMember(2)] public int BitcoinsAmount { get; set; }
    }
}