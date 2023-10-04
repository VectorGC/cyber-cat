using System.Collections.Generic;
using System.Linq;
using ProtoBuf;
using Shared.Models.Ids;

namespace Shared.Models.Models
{
    [ProtoInclude(100, typeof(SuccessV2))]
    [ProtoInclude(101, typeof(FailureV2))]
    [ProtoInclude(102, typeof(NativeFailure))]
    [ProtoContract]
    public abstract class VerdictV2
    {
    }

    [ProtoContract]
    public class SuccessV2 : VerdictV2
    {
        [ProtoMember(1)] public TestCasesVerdict TestCases { get; set; }
    }

    [ProtoContract]
    public class FailureV2 : VerdictV2
    {
        [ProtoMember(1)] public TestCasesVerdict TestCases { get; set; }
    }

    [ProtoContract]
    public class NativeFailure : VerdictV2
    {
        [ProtoMember(1)] public string Error { get; set; }
    }


    [ProtoInclude(100, typeof(SuccessTestCaseVerdict))]
    [ProtoInclude(101, typeof(FailureTestCaseVerdict))]
    [ProtoContract]
    public abstract class TestCaseVerdict
    {
        [ProtoMember(1)] public TestCase TestCase { get; set; }
        [ProtoMember(2)] public string Output { get; set; }
    }

    [ProtoContract]
    public class TestCases
    {
        [ProtoMember(1)] public Dictionary<TestCaseId, TestCase> Values { get; set; } = new Dictionary<TestCaseId, TestCase>();

        public TestCase this[string taskId, int index] => Values[new TestCaseId(taskId, index)];

        public void Add(TestCase test)
        {
            Values[test.Id] = test;
        }
    }

    [ProtoContract]
    public class SuccessTestCaseVerdict : TestCaseVerdict
    {
    }

    [ProtoContract]
    public class FailureTestCaseVerdict : TestCaseVerdict
    {
        public string Error => $"Expected result '{TestCase.Expected}', but was '{Output}'";
    }

    [ProtoContract]
    public class TestCase
    {
        [ProtoMember(1)] public TestCaseId Id { get; set; }
        [ProtoMember(2)] public string[] Inputs { get; set; }
        [ProtoMember(3)] public string Expected { get; set; }
    }

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