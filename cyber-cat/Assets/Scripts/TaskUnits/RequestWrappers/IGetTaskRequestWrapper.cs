using System;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json.Linq;
using ServerAPIBase;

namespace TaskUnits.RequestWrappers
{
    internal interface IGetTaskRequestWrapper
    {
        UniTask<ITaskDataCollection> GetTasks(string token, IProgress<float> progress = null);
        UniTask<JObject> GetTaskFolders(string token, IProgress<float> progress = null);
    }
}