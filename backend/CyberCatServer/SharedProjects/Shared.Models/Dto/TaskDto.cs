using ProtoBuf;

namespace Shared.Models.Dto
{
    [ProtoContract]
    public class TaskDto
    {
        [ProtoMember(1)]
        public string Name
        {
            get => name;
            set => name = value;
        }

        [ProtoMember(2)]
        public string Description
        {
            get => description;
            set => description = value;
        }

        public string name;
        public string description;

        public override string ToString()
        {
            return Name;
        }
    }
}