using ProtoBuf;

namespace Shared.Models.Models
{
    [ProtoContract]
    public class TaskId
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
    }
}