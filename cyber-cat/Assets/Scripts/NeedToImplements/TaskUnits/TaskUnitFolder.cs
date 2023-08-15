using System;
using ApiGateway.Client.Factory;
using Cysharp.Threading.Tasks;
using Features.GameManager;
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
            var client = await ServerClientFactory.UseCredentials("cat", "cat").Create(GameConfig.ServerEnvironment);
            var task = await client.Tasks.GetTask(_task);
            return TaskData.ConvertFrom(task);
        }
    }
}