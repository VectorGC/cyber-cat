using System;
using ApiGateway.Client;
using Cysharp.Threading.Tasks;
using TaskUnits.TaskDataModels;
using UnityEngine;

namespace TaskUnits
{
    [Serializable]
    public struct TaskUnitFolder
    {
        [SerializeField] private string _task;

        public TaskUnitFolder(string task)
        {
            _task = task;
        }

        public readonly async UniTask<ITaskData> GetTask()
        {
            var authorizationService = AuthorizationServiceFactory.Create(GameManager.ServerUri);
            var token = await authorizationService.Authenticate("cat", "cat");

            var repo = TaskRepositoryFactory.Create(GameManager.ServerUri, token);
            var task = await repo.GetTask(_task);

            return TaskData.ConvertFrom(task);
        }
    }
}