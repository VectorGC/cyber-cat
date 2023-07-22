using ProtoBuf;

namespace Shared.Models.Dto.Args
{
    [ProtoContract]
    public class PlayerIdArgs
    {
        [ProtoMember(1)] public long UserId { get; set; }
    }
}