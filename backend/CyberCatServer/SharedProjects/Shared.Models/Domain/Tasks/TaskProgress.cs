using ProtoBuf;

namespace Shared.Models.Domain.Tasks
{
    [ProtoContract]
    public class TaskProgress
    {
        [ProtoMember(1)] public TaskId TaskId { get; set; }
        [ProtoMember(2)] public TaskProgressStatusType StatusType { get; set; }
        [ProtoMember(3)] public string Solution { get; set; }

        public override string ToString()
        {
            return StatusType.ToString();
        }
    }
}