using ProtoBuf;

namespace Shared.Models.Domain.Tasks
{
    [ProtoContract]
    public class TaskProgress
    {
        [ProtoMember(1)]
        public TaskId TaskId
        {
            get => _taskId;
            set => _taskId = value;
        }

        [ProtoMember(2)]
        public TaskProgressStatusType StatusType
        {
            get => statusType;
            set => statusType = value;
        }

        [ProtoMember(3)]
        public string Solution
        {
            get => solution;
            set => solution = value;
        }

        private string _taskId;
        public string solution;
        public TaskProgressStatusType statusType;

        public override string ToString()
        {
            return StatusType.ToString();
        }
    }
}