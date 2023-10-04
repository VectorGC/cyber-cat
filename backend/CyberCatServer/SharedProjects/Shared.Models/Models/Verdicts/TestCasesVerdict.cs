using System.Collections.Generic;
using System.Linq;
using ProtoBuf;
using Shared.Models.Ids;
using Shared.Models.Models.TestCases;

namespace Shared.Models.Models.Verdicts
{
    [ProtoContract]
    public class TestCasesVerdict
    {
        [ProtoMember(1)] public Dictionary<TestCaseId, TestCaseVerdict> Values { get; set; } = new Dictionary<TestCaseId, TestCaseVerdict>();

        public TestCaseVerdict this[TestCaseId id] => Values[id];
        public TestCaseVerdict this[TaskId taskId, int index] => this[new TestCaseId(taskId, index)];
        public int PassedCount => Values.Values.Count(verdict => verdict is SuccessTestCaseVerdict);

        public void AddSuccess(TestCase testCase, string output)
        {
            Values[testCase.Id] = new SuccessTestCaseVerdict()
            {
                TestCase = testCase,
                Output = output
            };
        }

        public void AddFailure(TestCase testCase, string output)
        {
            Values[testCase.Id] = new FailureTestCaseVerdict()
            {
                TestCase = testCase,
                Output = output
            };
        }
    }
}