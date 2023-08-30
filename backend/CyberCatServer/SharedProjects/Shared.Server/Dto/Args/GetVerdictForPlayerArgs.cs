using ProtoBuf;
using Shared.Models.Dto;
using Shared.Server.Models;

namespace Shared.Server.Dto.Args
{
    [ProtoContract]
    public class GetVerdictForPlayerArgs
    {
        [ProtoMember(1)] public PlayerId PlayerId { get; set; }
        [ProtoMember(2)] public SolutionDto SolutionDto { get; set; }
    }
}