using ProtoBuf;
using Shared.Models.Models;

namespace Shared.Models.Dto
{
    [ProtoContract]
    public class SolutionDto
    {
        [ProtoMember(1)] public TaskId TaskId { get; set; }
        [ProtoMember(2)] public string SourceCode { get; set; }
    }
}