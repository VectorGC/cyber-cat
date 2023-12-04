using ProtoBuf;
using Shared.Models.Domain.TestCase;

namespace Shared.Models.Domain.Verdicts.TestCases
{
    [ProtoInclude(100, typeof(SuccessTestCaseVerdict))]
    [ProtoInclude(101, typeof(FailureTestCaseVerdict))]
    [ProtoContract]
    public abstract class TestCaseVerdict
    {
        [ProtoMember(1)] public TestCaseDescription TestCase { get; set; }
        [ProtoMember(2)] public string Output { get; set; }
    }
}