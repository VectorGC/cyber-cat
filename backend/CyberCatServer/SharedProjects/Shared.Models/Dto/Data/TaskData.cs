using ProtoBuf;
using Shared.Models.Enums;

namespace Shared.Models.Dto.Data
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

        public TaskProgressStatus status;

        public override string ToString()
        {
            return Status.ToString();
        }
    }
}