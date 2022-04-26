using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json.Linq;
using TasksData.RequestWrappers;

namespace TasksData
{
    public static class TaskUnitFacade
    {
        public static async UniTask<IEnumerable<ITaskData>> GetAllTasks(string token, IProgress<float> progress = null)
        {
            var tasksData = await new GetTaskRequestWrapper().GetTasks(token, progress);
            return tasksData.Values;
        }

        public static async UniTask<JObject> GetTaskFolders(string token, IProgress<float> progress)
        {
            var tasksByFolders = await new GetTaskRequestWrapper().GetTaskFolders(token, progress);
            return tasksByFolders;
        }
    }
}