using ProtoBuf;
using Shared.Models;

namespace Shared.Dto;

[ProtoContract]
public class TestsDto : IAdd<ITest>
{
    [ProtoMember(1)] public List<TestDto> Tests { get; set; } = new();

    public int Count => Tests.Count;

    public void Add(ITest item)
    {
        Tests.Add(item.To<TestDto>());
    }

    public List<TestDto>.Enumerator GetEnumerator() => Tests.GetEnumerator();
}