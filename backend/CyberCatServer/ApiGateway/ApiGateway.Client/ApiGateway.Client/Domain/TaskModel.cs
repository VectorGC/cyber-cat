using System;
using System.Collections.Generic;
using Shared.Models.Domain.Tasks;
using Shared.Models.Domain.TestCase;
using Shared.Models.Domain.Users;

namespace ApiGateway.Client.Domain
{
    public class TaskModel
    {
        public event Action<TaskModel> OnChanged;

        public TaskId Id => Description.Id;
        public TaskDescription Description { get; }
        public List<TestCaseDescription> TestCases { get; }
        public TaskProgressStatus Status { get; private set; }
        public IReadOnlyList<UserModel> UsersWhoSolvedTask => _usersWhoSolvedTask;

        private readonly List<UserModel> _usersWhoSolvedTask = new List<UserModel>();

        public TaskModel(TaskDescription description, TaskProgress progress, List<TestCaseDescription> testCases, IReadOnlyList<UserModel> usersWhoSolvedTask)
        {
            Description = description;
            TestCases = testCases;

            SetProgress(progress, usersWhoSolvedTask);
        }

        public void UpdateData(TaskModel taskModel)
        {
            Status = taskModel.Status;
        }

        public void SetProgress(TaskProgress progress, IReadOnlyList<UserModel> usersWhoSolvedTask)
        {
            Status = new TaskProgressStatus(progress);
            _usersWhoSolvedTask.Clear();
            _usersWhoSolvedTask.AddRange(usersWhoSolvedTask);

            OnChanged?.Invoke(this);
        }
    }
}