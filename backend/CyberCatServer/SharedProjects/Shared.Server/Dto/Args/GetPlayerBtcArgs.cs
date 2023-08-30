using ProtoBuf;
using Shared.Server.Models;

namespace Shared.Server.Dto.Args
{
    [ProtoContract]
    public class GetPlayerBtcArgs
    {
        [ProtoMember(1)] public PlayerId PlayerId { get; set; }
        [ProtoMember(2)] public int BitcoinsAmount { get; set; }
    }
}