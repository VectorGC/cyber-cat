using System;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using Newtonsoft.Json.Linq;
using RestAPIWrapper;
using TasksData;
using UnityEngine;

namespace Legacy_do_not_use_it
{
    [Serializable]
    public struct TaskUnitFolder : ITaskUnit
    {
        [SerializeField] private string _unit;
        [SerializeField] private string _task;

        public TaskUnitFolder(string unit, string task)
        {
            _unit = unit;
            _task = task;
        }

        public readonly async UniTask<bool?> IsTaskSolved(string token, IProgress<float> progress = null)
        {
            var task = await GetTask(token, progress);
            return task.IsSolved;
        }

        public readonly async UniTask<ITaskData> GetTask(string token, IProgress<float> progress = null)
        {
            var folders = await RestAPI.GetTaskFolders(token, progress);

            var taskJToken = GetTaskJToken(folders);
            var task = taskJToken?.ToObject<TaskData>();

            return (ITaskData) task ?? new EmptyTaskData();
        }

        [CanBeNull]
        private readonly JToken GetTaskJToken(JObject jObject)
        {
            var taskJToken = jObject.SelectToken($"sample_tests.units.{_unit}.tasks.{_task}");
            return taskJToken;
        }

        public override string ToString() => $"Unit: {_unit}, Task: {_task}";
    }
}