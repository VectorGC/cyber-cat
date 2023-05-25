using ProtoBuf;
using Shared.Models;

namespace Shared.Dto;

[ProtoContract]
public class TestsDto
{
    [ProtoMember(1)] public List<TestDto> Tests { get; set; }

    public List<TestDto>.Enumerator GetEnumerator()
    {
        return Tests.GetEnumerator();
    }
}

public static class TestsDtoExtensions
{
    public static TestsDto ToDto(this IEnumerable<ITest>? tests)
    {
        return new TestsDto
        {
            Tests = tests?.Select(test => test.ToDto()).ToList() ?? new List<TestDto>()
        };
    }
}