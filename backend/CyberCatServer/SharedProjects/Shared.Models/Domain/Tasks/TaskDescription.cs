using ProtoBuf;

namespace Shared.Models.Domain.Tasks
{
    [ProtoContract]
    public class TaskDescription
    {
        [ProtoMember(1)]
        public string Id
        {
            get => id = Id;
            set => id = value;
        }

        [ProtoMember(2)]
        public string Name
        {
            get => name;
            set => name = value;
        }

        [ProtoMember(3)]
        public string Description
        {
            get => description;
            set => description = value;
        }

        [ProtoMember(4)]
        public string DefaultCode
        {
            get => defaultCode;
            set => defaultCode = value;
        }

        public string id;
        public string name;
        public string description;
        public string defaultCode;

        public override string ToString()
        {
            return id;
        }
    }
}