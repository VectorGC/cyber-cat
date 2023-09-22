using System;
using ProtoBuf;

namespace Shared.Models.Ids
{
    [ProtoContract]
    public class TestCaseId : IEquatable<TestCaseId>, IComparable<TestCaseId>
    {
        [ProtoMember(1)] public int Value { get; set; }

        public TestCaseId(int id)
        {
            Value = id;
        }

        public TestCaseId()
        {
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        public static implicit operator TestCaseId(int id)
        {
            return new TestCaseId(id);
        }

        public static implicit operator int(TestCaseId obj)
        {
            return obj.Value;
        }

        public bool Equals(TaskId x, TaskId y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null)) return false;
            if (ReferenceEquals(y, null)) return false;
            if (x.GetType() != y.GetType()) return false;
            return x.Value == y.Value;
        }

        public int GetHashCode(TaskId obj)
        {
            return (obj.Value != null ? obj.Value.GetHashCode() : 0);
        }

        public bool Equals(TestCaseId other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Value == other.Value;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public int CompareTo(TestCaseId other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            return Value.CompareTo(other.Value);
        }
    }
}