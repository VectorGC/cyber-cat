using ProtoBuf;
using Shared.Models;

namespace Shared.Dto;

[ProtoContract]
public class SolutionDto : ISolution
{
    [ProtoMember(1)] public string TaskId { get; set; }
    [ProtoMember(2)] public string SourceCode { get; set; }

    public SolutionDto(ISolution solution)
    {
        TaskId = solution.TaskId;
        SourceCode = solution.SourceCode;
    }

    public SolutionDto()
    {
    }
}