using System;
using Shared.Models.Domain.Tasks;

namespace ApiGateway.Client.V3.Domain
{
    public class TaskModel
    {
        public event Action<TaskModel> OnChanged;

        public TaskId Id => Description.Id;
        public TaskDescription Description { get; }
        public TaskProgressStatus ProgressStatus { get; private set; }

        public TaskModel(TaskDescription description, TaskProgress progress)
        {
            Description = description;
            ProgressStatus = new TaskProgressStatus(progress);
        }

        public void SetProgress(TaskProgress progress)
        {
            ProgressStatus = new TaskProgressStatus(progress);
            OnChanged?.Invoke(this);
        }
    }
}