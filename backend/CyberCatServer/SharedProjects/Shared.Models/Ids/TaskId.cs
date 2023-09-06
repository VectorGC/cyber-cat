using System;
using ProtoBuf;

namespace Shared.Models.Ids
{
    [ProtoContract]
    public class TaskId : IEquatable<TaskId>
    {
        [ProtoMember(1)] public string Value { get; set; }

        public TaskId(string id)
        {
            Value = id;
        }

        public TaskId()
        {
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        public static implicit operator TaskId(string id)
        {
            return new TaskId(id);
        }

        public static implicit operator string(TaskId taskId)
        {
            return taskId.Value;
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

        public bool Equals(TaskId other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Value == other.Value;
        }

        public override int GetHashCode()
        {
            return (Value != null ? Value.GetHashCode() : 0);
        }
    }
}