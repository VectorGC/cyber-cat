using ProtoBuf;

namespace Shared.Models.Dto.Args
{
    [ProtoContract]
    public class PlayerIdArgs
    {
        [ProtoMember(1)] public string PlayerId { get; set; }
    }
}