using ProtoBuf;

namespace Shared.Models.Domain.Tasks
{
    [ProtoContract]
    public class TaskDescription
    {
        [ProtoMember(1)] public string Id { get; set; }
        [ProtoMember(2)] public string Name { get; set; }
        [ProtoMember(3)] public string Description { get; set; }
        [ProtoMember(4)] public string DefaultCode { get; set; }

        public override string ToString()
        {
            return Id;
        }
    }
}