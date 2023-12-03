using System.Collections.Generic;
using ProtoBuf;
using Shared.Models.Domain.TestCase;

namespace Shared.Models.TO_REMOVE
{
    [ProtoContract]
    public class TestCases
    {
        [ProtoMember(1)] public Dictionary<string, TestCaseDescription> Values { get; set; } = new Dictionary<string, TestCaseDescription>();

        public TestCaseDescription this[string taskId, int index] => Values[new TestCaseId(taskId, index)];

        public void Add(TestCaseDescription test)
        {
            Values[test.Id] = test;
        }
    }
}