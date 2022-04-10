using System;
using Authentication;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json.Linq;
using RestAPIWrapper;
using TasksData;
using UniRx;
using UnityEngine;

namespace Legacy_do_not_use_it
{
    [Serializable]
    public class TaskUnitFolder : ITaskUnit
    {
        [SerializeField] private string _unit;
        [SerializeField] private string _task;

        private readonly Subject<ITaskData> _subject;

        public TaskUnitFolder(string unit, string task)
        {
            _unit = unit;
            _task = task;
            _subject = new Subject<ITaskData>();
        }

        public async UniTask<ITaskData> GetTask(IProgress<float> progress = null)
        {
            var token = TokenSession.FromPlayerPrefs();
            return await GetTask(token, progress);
        }

        public async UniTask<bool> IsTaskSolved(IProgress<float> progress = null)
        {
            var token = TokenSession.FromPlayerPrefs();
            var task = await GetTask(token, progress);

            return task.IsSolved;
        }

        public async UniTask CallTaskChanged()
        {
            var task = await GetTask();
            _subject.OnNext(task);
        }

        private async UniTask<ITaskData> GetTask(string token, IProgress<float> progress = null)
        {
            var folders = await RestAPI.GetTaskFolders(token, progress);

            var taskJToken = GetTaskJToken(folders);
            var task = taskJToken.ToObject<TaskData>();

            return task;
        }

        private JToken GetTaskJToken(JObject jObject)
        {
            var taskJToken = jObject["sample_tests"]["units"][_unit]["tasks"][_task];
            return taskJToken;
        }

        public IDisposable Subscribe(IObserver<ITaskData> observer) => _subject.Subscribe(observer);
    }
}