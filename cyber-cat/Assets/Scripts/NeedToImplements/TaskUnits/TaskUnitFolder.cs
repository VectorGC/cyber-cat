using System;
using Cysharp.Threading.Tasks;
using ServerAPI;
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
            var client = ServerAPIFacade.Create();
            var token = await client.Authenticate("cat", "cat");
            client.AddAuthorizationToken(token);

            var task = await client.GetTask(_task);

            return TaskData.ConvertFrom(task);
        }
    }
}