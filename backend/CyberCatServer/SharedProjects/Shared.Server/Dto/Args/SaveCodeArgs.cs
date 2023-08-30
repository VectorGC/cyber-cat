using ProtoBuf;
using Shared.Models.Dto;
using Shared.Server.Models;

namespace Shared.Server.Dto.Args
{
    [ProtoContract]
    public class SaveCodeArgs
    {
        [ProtoMember(1)] public UserId UserId { get; set; }
        [ProtoMember(2)] public SolutionDto Solution { get; set; }
    }
}