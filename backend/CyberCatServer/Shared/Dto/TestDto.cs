using ProtoBuf;
using Shared.Models;

namespace Shared.Dto;

[ProtoContract]
public class TestDto
{
    [ProtoMember(1)] public string Input { get; set; }
    [ProtoMember(2)] public string ExpectedOutput { get; set; }
}

public static class TestDtoExtensions
{
    public static TestDto ToDto(this ITest test)
    {
        return new TestDto
        {
            Input = test.Input,
            ExpectedOutput = test.ExpectedOutput
        };
    }

    public static ListProto<TestDto> ToProtoDto(this List<ITest> tests)
    {
        return tests.Select(test => test.ToDto()).ToListProto();
    }
}