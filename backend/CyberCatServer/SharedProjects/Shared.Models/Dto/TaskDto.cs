using ProtoBuf;
using Shared.Models;

namespace Shared.Dto
{
    [ProtoContract]
    public class TaskDto : ITask
    {
        [ProtoMember(1)] public string Name { get; set; }

        [ProtoMember(2)] public string Description { get; set; }

        public TaskDto(ITask task)
        {
            Name = task.Name;
            Description = task.Description;
        }

        public TaskDto()
        {
        }
    }
}