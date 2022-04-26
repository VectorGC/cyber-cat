using System;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Proyecto26;
using UnityEngine;

namespace TasksData.RequestWrappers
{
    public struct GetTaskRequestWrapper : IGetTaskRequestWrapper
    {
        public async UniTask<TasksData> GetTasks(string token, IProgress<float> progress = null)
        {
            var request = new RequestHelper
            {
                Uri = Endpoint.TASKS_FLAT,
                Params =
                {
                    ["token"] = token
                },
                ProgressCallback = value => progress?.Report(value),
                EnableDebug = Debug.isDebugBuild
            };

            return await RestClient.Get<TasksData>(request).ToUniTask();
        }

        public async UniTask<JObject> GetTaskFolders(string token, IProgress<float> progress = null)
        {
            var request = new RequestHelper
            {
                Uri = Endpoint.TASKS_HIERARCHY,
                Params =
                {
                    ["token"] = token,
                },
                ProgressCallback = value => progress?.Report(value),
                EnableDebug = Debug.isDebugBuild
            };

            return await RestClient.Get<JObject>(request).ToUniTask();
        }
    }
}