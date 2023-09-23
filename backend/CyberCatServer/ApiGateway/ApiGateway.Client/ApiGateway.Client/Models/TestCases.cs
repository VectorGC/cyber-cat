using System.Collections.Generic;
using System.Linq;
using Shared.Models.Ids;

namespace ApiGateway.Client.Models
{
    public class TestCases
    {
        public SortedDictionary<TestCaseId, ITestCase>.KeyCollection Ids => _dictionary.Keys;

        private readonly SortedDictionary<TestCaseId, ITestCase> _dictionary = new SortedDictionary<TestCaseId, ITestCase>();

        public ITestCase this[TestCaseId id]
        {
            get => _dictionary[id];
            set => _dictionary[id] = value;
        }
    }

    public class TestCasesVerdict : IVerdictV2
    {
        public ITestCaseVerdict this[TestCaseId id] => _verdicts[id];
        public Dictionary<TestCaseId, ITestCaseVerdict>.KeyCollection Ids => _verdicts.Keys;
        public int PassedCount => _verdicts.Values.Count(verdict => verdict is SuccessTestCaseVerdict);

        private readonly Dictionary<TestCaseId, ITestCaseVerdict> _verdicts = new Dictionary<TestCaseId, ITestCaseVerdict>();

        public TestCasesVerdict(TestCasesVerdictData data, TestCases testCases)
        {
            foreach (var kvp in data.Verdicts)
            {
                var id = kvp.Key;
                var testCase = testCases[id];
                var verdictData = kvp.Value;

                var verdict = verdictData.IsSuccess
                    ? (ITestCaseVerdict) new SuccessTestCaseVerdict(testCase, verdictData)
                    : new FailureTestCaseVerdict(testCase, verdictData);

                _verdicts[id] = verdict;
            }
        }
    }

    public class TestCasesVerdictData
    {
        public Dictionary<TestCaseId, TestCaseVerdictData> Verdicts { get; set; }
    }

    public class TestCaseVerdictData
    {
        public object Output { get; set; }
        public bool IsSuccess { get; set; }
    }

    public interface ITestCaseVerdict
    {
        ITestCase TestCase { get; }
        object Output { get; }
    }

    public class SuccessTestCaseVerdict : ITestCaseVerdict
    {
        public ITestCase TestCase { get; }

        public object Output => _data.Output;
        private readonly TestCaseVerdictData _data;

        public SuccessTestCaseVerdict(ITestCase testCase, TestCaseVerdictData data)
        {
            _data = data;
            TestCase = testCase;
        }
    }

    public class FailureTestCaseVerdict : ITestCaseVerdict
    {
        public ITestCase TestCase { get; }

        public object Output => _data.Output;

        private readonly TestCaseVerdictData _data;

        public FailureTestCaseVerdict(ITestCase testCase, TestCaseVerdictData data)
        {
            _data = data;
            TestCase = testCase;
        }
    }
}