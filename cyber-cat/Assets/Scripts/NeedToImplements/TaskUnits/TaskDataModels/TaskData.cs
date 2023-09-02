using System;
using Shared.Models.Dto.Descriptions;

namespace TaskUnits.TaskDataModels
{
    internal class TaskData : ITaskData
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Output { get; set; }
        public float TotalScore { get; set; }

        private float _completion;

        public float ReceivedScore => _completion * TotalScore;
        public bool? IsSolved => _completion >= 1f;

        // TODO:
        [Obsolete("Do not use it, it will be removed. It is only needed for compatibility of the old ITaskData with the new service architecture ITask.")]
        public static ITaskData ConvertFrom(TaskDescription task)
        {
            return new TaskData
            {
                Name = task.Name,
                Description = task.Description
            };
        }
    }
}