using ProtoBuf;
using Shared.Models;

namespace Shared.Dto;

[ProtoContract]
public class TestDto : ITest
{
    [ProtoMember(1)] public string Input { get; init; }
    [ProtoMember(2)] public string ExpectedOutput { get; init; }
}