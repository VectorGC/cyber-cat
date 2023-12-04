using System;
using System.Collections.Generic;
using Shared.Models.Domain.Tasks;
using Shared.Models.Domain.TestCase;

namespace ApiGateway.Client.Domain
{
    public class TaskModel
    {
        public event Action<TaskModel> OnChanged;

        public TaskId Id => Description.Id;
        public TaskDescription Description { get; }
        public List<TestCaseDescription> TestCases { get; }
        public TaskProgressStatus Status { get; private set; }

        public TaskModel(TaskDescription description, TaskProgress progress, List<TestCaseDescription> testCases)
        {
            Description = description;
            TestCases = testCases;
            Status = new TaskProgressStatus(progress);
        }

        public void SetProgress(TaskProgress progress)
        {
            Status = new TaskProgressStatus(progress);
            OnChanged?.Invoke(this);
        }
    }
}