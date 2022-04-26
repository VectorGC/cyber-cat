using System;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace TasksData.RequestWrappers
{
    public interface IGetTaskRequestWrapper
    {
        UniTask<global::TasksData.TasksData> GetTasks(string token, IProgress<float> progress = null);
        public UniTask<JObject> GetTaskFolders(string token, IProgress<float> progress = null);
    }
}