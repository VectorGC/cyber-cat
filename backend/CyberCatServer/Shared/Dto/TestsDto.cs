using ProtoBuf;
using Shared.Models;

namespace Shared.Dto;

[ProtoContract]
public class TestsDto
{
    [ProtoMember(1)] public List<TestDto> Tests { get; set; } = new();

    public TestsDto(ITests tests)
    {
        Tests = tests.Select(test => new TestDto(test)).ToList();
    }

    public TestsDto()
    {
    }

    public List<TestDto>.Enumerator GetEnumerator() => Tests.GetEnumerator();
}