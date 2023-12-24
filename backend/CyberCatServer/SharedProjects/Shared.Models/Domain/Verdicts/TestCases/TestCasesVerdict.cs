using System.Collections.Generic;
using System.Linq;
using ProtoBuf;
using Shared.Models.Domain.Tasks;
using Shared.Models.Domain.TestCase;

namespace Shared.Models.Domain.Verdicts.TestCases
{
    [ProtoContract]
    public class TestCasesVerdict
    {
        [ProtoMember(1)] public Dictionary<string, TestCaseVerdict> Values { get; set; } = new Dictionary<string, TestCaseVerdict>();

        public TestCaseVerdict this[TestCaseId id] => Values[id];
        public TestCaseVerdict this[TaskId taskId, int index] => this[new TestCaseId(taskId, index)];
        public int PassedCount => Values.Values.Count(verdict => verdict is SuccessTestCaseVerdict);
        public bool IsSuccess => Values.Count > 0 && Values.Values.All(verdict => verdict is SuccessTestCaseVerdict);

        public void AddSuccess(TestCaseDescription testCase, string output)
        {
            Values[testCase.Id] = new SuccessTestCaseVerdict()
            {
                TestCase = testCase,
                Output = output
            };
        }

        public void AddFailure(TestCaseDescription testCase, string output)
        {
            Values[testCase.Id] = new FailureTestCaseVerdict()
            {
                TestCase = testCase,
                Output = output
            };
        }
    }
}