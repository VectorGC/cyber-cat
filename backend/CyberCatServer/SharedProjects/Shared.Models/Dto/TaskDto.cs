using ProtoBuf;
using Shared.Models.Dto.Data;
using Shared.Models.Dto.Descriptions;

namespace Shared.Models.Dto
{
    [ProtoContract]
    public class TaskDto
    {
        [ProtoMember(1)]
        public TaskDescription Description
        {
            get => description;
            set => description = value;
        }

        [ProtoMember(2)]
        public TaskData Data
        {
            get => data;
            set => data = value;
        }

        public TaskDescription description;
        public TaskData data;

        public override string ToString()
        {
            return Description.Name;
        }
    }
}