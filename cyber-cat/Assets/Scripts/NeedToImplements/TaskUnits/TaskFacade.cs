using System;
using Cysharp.Threading.Tasks;
using ServerAPI;
using TaskUnits.TaskDataModels;

namespace TaskUnits
{
    public static class TaskFacade
    {
        internal static async UniTask<ITaskData> GetTask(string token, string taskId, IProgress<float> progress = null)
        {
            var client = await ServerEnvironment.CreateAuthorizedClient();
            var task = await client.Tasks.GetTask(taskId);
            return TaskData.ConvertFrom(task);
        }
    }
}