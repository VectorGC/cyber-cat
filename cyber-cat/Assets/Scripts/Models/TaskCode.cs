using Shared.Dto;

namespace Models
{
    public class TaskCode : ITask
    {
        public string Name { get; }

        public string Description { get; }

        public TaskCode(TaskDto taskDto)
        {
            Name = taskDto.Name;
            Description = taskDto.Description;
        }
    }
}