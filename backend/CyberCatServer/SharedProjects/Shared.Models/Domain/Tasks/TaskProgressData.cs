using ProtoBuf;
using Shared.Models.Enums;
using Shared.Models.Ids;

namespace Shared.Models.Domain.Tasks
{
    [ProtoContract]
    public class TaskProgressData
    {
        [ProtoMember(1)] public TaskId TaskId { get; set; }

        [ProtoMember(2)]
        public TaskProgressStatus Status
        {
            get => status;
            set => status = value;
        }

        [ProtoMember(3)]
        public string Solution
        {
            get => solution;
            set => solution = value;
        }

        public string solution;
        public TaskProgressStatus status;

        public override string ToString()
        {
            return Status.ToString();
        }
    }
}