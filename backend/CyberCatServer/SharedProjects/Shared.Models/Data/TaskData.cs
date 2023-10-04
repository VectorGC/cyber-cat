using ProtoBuf;
using Shared.Models.Enums;

namespace Shared.Models.Data
{
    [ProtoContract]
    public class TaskData
    {
        [ProtoMember(1)]
        public TaskProgressStatus Status
        {
            get => status;
            set => status = value;
        }

        [ProtoMember(2)]
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