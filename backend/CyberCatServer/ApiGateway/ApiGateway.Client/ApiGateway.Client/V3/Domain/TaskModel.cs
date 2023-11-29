using System;
using Shared.Models.Domain.Tasks;
using Shared.Models.Ids;

namespace ApiGateway.Client.V3.Domain
{
    public class TaskModel
    {
        public event Action<TaskModel> OnChanged;

        public TaskId Id => Description.Id;
        public TaskDescription Description { get; }
        public TaskStatus Status { get; private set; }

        public TaskModel(TaskDescription description, TaskProgressData progressData)
        {
            Description = description;
            Status = new TaskStatus(progressData);
        }

        public void SetProgress(TaskProgressData progressData)
        {
            Status = new TaskStatus(progressData);
            OnChanged?.Invoke(this);
        }
    }
}