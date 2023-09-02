using System;
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
            var player = await GameConfig.GetOrPlayerClient();
            var task = player.Tasks[_task];
            var name = await task.GetName();
            var description = await task.GetDescription();

            return new TaskData
            {
                Name = name,
                Description = description
            };
        }
    }
}