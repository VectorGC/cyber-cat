using ProtoBuf;

namespace Shared.Models.Dto.Args
{
    [ProtoContract]
    public class PlayerArgs
    {
        [ProtoMember(1)] public long UserId { get; set; }
    }
}