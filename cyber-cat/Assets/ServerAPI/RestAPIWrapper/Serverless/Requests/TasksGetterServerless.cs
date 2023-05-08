using Cysharp.Threading.Tasks;
using Proyecto26;
using ServerAPIBase;
using System;
using UnityEngine;

namespace RestAPIWrapper.Serverless
{
    public class TasksGetterServerless : ITasksGetter<string>
    {
        private const string TasksFlatJson = "tasks_flat.json";
        private string _taskFlat;

        public void Request(ITasksGetterData data, Action<string> callback)
        {
            ReadFileContent(callback);
        }

        public async UniTask<string> RequestAsync(ITasksGetterData data, IProgress<float> progress = null)
        {
            if (string.IsNullOrEmpty(_taskFlat))
            {
                _taskFlat = await ReadFileContentAsync();
            }

            return _taskFlat;
        }

        private async UniTask<string> ReadFileContentAsync()
        {
            var filePath = System.IO.Path.Combine(Application.streamingAssetsPath, TasksFlatJson);
            if (filePath.Contains("://"))
            {
                var response = await RestClient.Get(filePath).ToUniTask();
                return response.Text;
            }

            return System.IO.File.ReadAllText(filePath);
        }

        private void ReadFileContent(Action<string> callback)
        {
            var filePath = System.IO.Path.Combine(Application.streamingAssetsPath, TasksFlatJson);
            if (filePath.Contains("://"))
            {
                RestClient.Get(filePath).Done(x => callback?.Invoke(x.Text));
            }

            callback?.Invoke(System.IO.File.ReadAllText(filePath));
        }
    }
}