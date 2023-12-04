using ProtoBuf;

namespace Shared.Models.Domain.TestCase
{
    [ProtoContract]
    public class TestCaseDescription
    {
        [ProtoMember(1)] public TestCaseId Id { get; set; }
        [ProtoMember(2)] public string[] Inputs { get; set; }
        [ProtoMember(3)] public string Expected { get; set; }
    }
}