using System;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestAPIWrapper;

namespace TaskUnits.RequestWrappers
{
    internal struct GetTaskRequestWrapper : IGetTaskRequestWrapper
    {
        public async UniTask<ITaskDataCollection> GetTasks(string token, IProgress<float> progress = null)
        {
            var responseText = await RestAPI.Instance.GetTasks(token, progress);
            return JsonConvert.DeserializeObject<TaskDataModels.TasksData>(responseText);
        }

        public async UniTask<JObject> GetTaskFolders(string token, IProgress<float> progress = null)
        {
            return await RestAPI.Instance.GetTaskFolders(token, progress);
        }
    }
}