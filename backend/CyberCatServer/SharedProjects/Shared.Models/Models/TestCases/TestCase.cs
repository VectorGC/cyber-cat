using ProtoBuf;
using Shared.Models.Ids;

namespace Shared.Models.Models.TestCases
{
    [ProtoContract]
    public class TestCase
    {
        [ProtoMember(1)] public TestCaseId Id { get; set; }
        [ProtoMember(2)] public string[] Inputs { get; set; }
        [ProtoMember(3)] public string Expected { get; set; }
    }
}