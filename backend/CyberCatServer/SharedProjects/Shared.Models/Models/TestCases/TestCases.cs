using System.Collections.Generic;
using ProtoBuf;
using Shared.Models.Ids;

namespace Shared.Models.Models.TestCases
{
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
}