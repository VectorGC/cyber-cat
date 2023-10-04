using ProtoBuf;

namespace Shared.Models.Models.Verdicts
{
    [ProtoContract]
    public class FailureTestCaseVerdict : TestCaseVerdict
    {
        public string Error => $"Expected result '{TestCase.Expected}', but was '{Output}'";
    }
}