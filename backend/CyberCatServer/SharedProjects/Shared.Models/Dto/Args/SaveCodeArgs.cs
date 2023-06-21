using ProtoBuf;

namespace Shared.Models.Dto.Args
{
    [ProtoContract]
    public class SaveCodeArgs
    {
        [ProtoMember(1)] public string UserId { get; set; }
        [ProtoMember(2)] public SolutionDto Solution { get; set; }
    }
}