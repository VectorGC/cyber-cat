using ProtoBuf;
using Shared.Models;

namespace Shared.Dto;

[ProtoContract]
public class SolutionDto : ISolution
{
    [ProtoMember(1)] public string UserId { get; init; }
    [ProtoMember(2)] public string TaskId { get; init; }
    [ProtoMember(3)] public string SourceCode { get; init; }
}