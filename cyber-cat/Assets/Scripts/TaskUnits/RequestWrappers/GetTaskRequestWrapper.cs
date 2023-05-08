using System;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json.Linq;
using RestAPIWrapper;

namespace TaskUnits.RequestWrappers
{
    internal struct GetTaskRequestWrapper : IGetTaskRequestWrapper
    {
        public async UniTask<ITaskDataCollection> GetTasks(string token, IProgress<float> progress = null)
        {
            return await RestAPI.Instance.GetTasks(token, progress);
        }

        public async UniTask<JObject> GetTaskFolders(string token, IProgress<float> progress = null)
        {
            return await RestAPI.Instance.GetTaskFolders(token, progress);
        }
    }
}