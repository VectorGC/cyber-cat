using System;
using System.Net;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestAPIWrapper;
using TasksData;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Legacy_do_not_use_it
{
    [Serializable]
    public struct TaskUnitFolder : ITaskUnit
    {
        [SerializeField] private string _unit;
        [SerializeField] private string _task;

        public string Unit => _unit;

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

            if (folders.GetValue("error") != null)
            {
                var error = folders.GetValue("error").ToString();
                var code = folders.GetValue("error").ToString();
                var localizedError = WebErrorLocalize.Localize(code);
                
                ModalPanel.ShowModalDialog("Непредвиденная ошибка",
                    $"Пожалуйста, сообщите организатору ошибку:\n{localizedError}", () =>
                    {
                        var async = SceneManager.LoadSceneAsync("StartScene");
                        async.ViaLoadingScreen();
                    });

                throw new WebException(error);
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