using ProtoBuf;
using Shared.Models.Domain.Tasks;
using Shared.Models.Domain.Verdicts.TestCases;

namespace Shared.Models.Domain.Verdicts
{
    [ProtoInclude(100, typeof(NativeFailure))]
    [ProtoContract]
    public class Verdict
    {
        [ProtoMember(1)] public TaskId TaskId { get; set; }
        [ProtoMember(2)] public string Solution { get; set; }
        [ProtoMember(3)] public TestCasesVerdict TestCases { get; set; } = new TestCasesVerdict();

        public bool IsSuccess => TestCases.IsSuccess;
        public bool IsFailure => !IsSuccess;
    }
}