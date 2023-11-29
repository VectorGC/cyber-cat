using ProtoBuf;
using Shared.Models.Models.TestCases;

namespace Shared.Models.Domain.Verdicts
{
    [ProtoInclude(100, typeof(SuccessTestCaseVerdict))]
    [ProtoInclude(101, typeof(FailureTestCaseVerdict))]
    [ProtoContract]
    public abstract class TestCaseVerdict
    {
        [ProtoMember(1)] public TestCase TestCase { get; set; }
        [ProtoMember(2)] public string Output { get; set; }
    }
}