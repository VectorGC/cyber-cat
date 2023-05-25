using System.Collections;
using ProtoBuf;
using Shared.Models;

namespace Shared.Dto;

[ProtoContract]
public class TestsDto : ITests
{
    [ProtoMember(1)] public List<TestDto> Tests { get; set; }

    IReadOnlyList<ITest> ITests.Tests
    {
        get => Tests;
        init => Tests = value.Select(test => test.To<TestDto>()).ToList();
    }
}