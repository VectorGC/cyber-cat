using ProtoBuf;

namespace Shared.Dto;

[ProtoContract]
public class GetSavedCodeResponse
{
    [ProtoMember(1)] public string SolutionCode { get; set; }
}