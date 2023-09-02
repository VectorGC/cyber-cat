using System;
using ProtoBuf;

namespace Shared.Models.Ids
{
    [ProtoContract]
    public class TaskId : IEquatable<TaskId>
    {
        [ProtoMember(1)]
        public string Value
        {
            get => value;
            set => this.value = value;
        }

        public string value;

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
            return x.value == y.value;
        }

        public int GetHashCode(TaskId obj)
        {
            return (obj.value != null ? obj.value.GetHashCode() : 0);
        }

        public bool Equals(TaskId other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return value == other.value;
        }

        public override int GetHashCode()
        {
            return (value != null ? value.GetHashCode() : 0);
        }
    }
}