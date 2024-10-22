using System;
using System.Linq;
using ProtoBuf;
using Shared.Models.Domain.Tasks;

namespace Shared.Models.Domain.TestCase
{
    [ProtoContract]
    public class TestCaseId : IEquatable<TestCaseId>
    {
        [ProtoMember(1)] public TaskId TaskId { get; set; }
        [ProtoMember(2)] public int Index { get; set; }

        public TestCaseId(TaskId taskId, int index)
        {
            TaskId = taskId;
            Index = index;
        }

        public TestCaseId()
        {
        }

        public override string ToString()
        {
            return $"{TaskId}:[{Index}]";
        }

        public void Deconstruct(out string taskId, out int index)
        {
            taskId = TaskId;
            index = Index;
        }

        public static implicit operator string(TestCaseId id)
        {
            return $"{id.TaskId}:{id.Index}";
        }

        public static implicit operator TestCaseId(string str)
        {
            var splitted = str.Split(':').ToList();
            var index = int.Parse(splitted[1]);
            return new TestCaseId(splitted[0], index);
        }

        public bool Equals(TestCaseId other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(TaskId, other.TaskId) && Index == other.Index;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((TestCaseId) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((TaskId != null ? TaskId.GetHashCode() : 0) * 397) ^ Index;
            }
        }
    }
}