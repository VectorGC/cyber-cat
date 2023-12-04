using ProtoBuf;

namespace Shared.Models.Domain.Verdicts.TestCases
{
    [ProtoContract]
    public class FailureTestCaseVerdict : TestCaseVerdict
    {
        public string Error => $"Expected result '{TestCase.Expected}', but was '{Output}'";
    }
}