using ProtoBuf;

namespace Shared.Models.Descriptions
{
    [ProtoContract]
    public class TaskDescription
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

        [ProtoMember(3)]
        public string DefaultCode
        {
            get => defaultCode;
            set => defaultCode = value;
        }

        public string name;
        public string description;
        public string defaultCode;

        public override string ToString()
        {
            return Name;
        }
    }
}