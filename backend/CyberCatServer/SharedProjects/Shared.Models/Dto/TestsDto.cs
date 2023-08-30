using System.Collections.Generic;
using ProtoBuf;

namespace Shared.Models.Dto
{
    [ProtoContract]
    public class TestsDto
    {
        [ProtoMember(1)] public List<TestDto> Tests { get; set; } = new List<TestDto>();

        public List<TestDto>.Enumerator GetEnumerator() => Tests.GetEnumerator();
    }
}