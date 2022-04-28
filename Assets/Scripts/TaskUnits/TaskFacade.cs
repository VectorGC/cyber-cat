using System;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json.Linq;
using TaskUnits.RequestWrappers;
using TaskUnits.TaskDataModels;

namespace TaskUnits
{
    public static class TaskFacade
    {
        public static async UniTask<ITaskDataCollection> GetAllTasks(string token, IProgress<float> progress = null)
        {
            return await new GetTaskRequestWrapper().GetTasks(token, progress);
        }

        internal static async UniTask<JObject> GetTaskFolders(string token, IProgress<float> progress)
        {
            var tasksByFolders = await new GetTaskRequestWrapper().GetTaskFolders(token, progress);
            return tasksByFolders;
        }

        internal static async UniTask<ITaskData> GetTask(string token, string taskId, IProgress<float> progress = null)
        {
            var tasks = await new GetTaskRequestWrapper().GetTasks(token, progress);
            var success = tasks.TryGetValue(taskId, out var taskData);
            if (!success)
            {
                return new EmptyTaskData();
            }

            return taskData;
        }
    }
}