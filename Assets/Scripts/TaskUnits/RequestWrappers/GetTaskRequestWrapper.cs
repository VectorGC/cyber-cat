using System;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Proyecto26;
using TaskUnits;
using UnityEngine;

namespace TasksData.RequestWrappers
{
    internal struct GetTaskRequestWrapper : IGetTaskRequestWrapper
    {
        public async UniTask<ITaskDataCollection> GetTasks(string token, IProgress<float> progress = null)
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

            return await RestClient.Get<TaskUnits.TasksData.TasksData>(request).ToUniTask();
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