using System;
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
            //var t = new HttpClient();
            _task = task;
        }

        public readonly async UniTask<ITaskData> GetTask()
        {
            var task = await GameManager.Instance.TaskRepository.GetTask(_task);
            return TaskData.ConvertFrom(task);
        }
    }
}