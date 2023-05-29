using ProtoBuf;
using Shared.Models;

namespace Shared.Dto;

[ProtoContract]
public class TestDto : ITest
{
    [ProtoMember(1)] public string Input { get; set; }
    [ProtoMember(2)] public string ExpectedOutput { get; set; }

    public TestDto(ITest test)
    {
        Input = test.Input;
        ExpectedOutput = test.ExpectedOutput;
    }

    public TestDto()
    {
    }
}