using System;
using Shared.Models.Dto;

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
        [Obsolete("Не используйте его, он будет удален. Он нужен только для совеместимости старой ITaskData с новой сервисной архитекутрой ITask")]
        public static ITaskData ConvertFrom(TaskDto task)
        {
            return new TaskData
            {
                Name = task.Name,
                Description = task.Description
            };
        }
    }
}