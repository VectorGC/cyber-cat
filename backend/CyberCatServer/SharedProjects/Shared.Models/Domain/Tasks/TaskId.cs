using System;
using ProtoBuf;

namespace Shared.Models.Domain.Tasks
{
    [ProtoContract]
    public class TaskId : IEquatable<TaskId>
    {
        [ProtoMember(1)] public string Value { get; set; } = string.Empty;

        public TaskId(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                var t = 10;
            }
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

        public bool Equals(TaskId other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Value == other.Value;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((TaskId) obj);
        }

        public override int GetHashCode()
        {
            return (Value != null ? Value.GetHashCode() : 0);
        }
    }
}