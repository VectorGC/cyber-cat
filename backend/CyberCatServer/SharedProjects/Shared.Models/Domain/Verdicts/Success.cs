using ProtoBuf;

namespace Shared.Models.Domain.Verdicts
{
    [ProtoContract]
    public class Success : Verdict
    {
        [ProtoMember(1)] public TestCasesVerdict TestCases { get; set; }
    }
}