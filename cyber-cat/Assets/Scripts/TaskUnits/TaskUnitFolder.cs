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
            _task = task;
        }

        public readonly async UniTask<ITaskData> GetTask(IProgress<float> progress = null)
        {
            var task = await GameManager.Instance.TaskRepository.GetTask(_task, progress);
            return TaskData.ConvertFrom(task);
        }
    }
}