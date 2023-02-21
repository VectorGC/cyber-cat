using System;
using System.Net;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using Newtonsoft.Json.Linq;
using RestAPIWrapper;
using TaskUnits.TaskDataModels;
using UnityEngine;

namespace TaskUnits
{
    [Serializable]
    public struct TaskUnitFolder
    {
        [SerializeField] private string _unit;
        [SerializeField] private string _task;

        public TaskUnitFolder(string unit, string task)
        {
            _unit = unit;
            _task = task;
        }

        public readonly async UniTask<ITaskData> GetTask(string token, IProgress<float> progress = null)
        {
            var folders = await TaskFacade.GetTaskFolders(token, progress);

            if (folders.GetValue("error") != null)
            {
                var errorCode = folders.GetValue("error").ToString();
                var localizedError = WebErrorLocalize.Localize(errorCode);

                throw new WebException($"Пожалуйста, сообщите организатору ошибку:\n{localizedError}");
            }

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