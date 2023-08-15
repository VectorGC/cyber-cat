using System;
using ApiGateway.Client.Factory;
using Cysharp.Threading.Tasks;
using Features.GameManager;
using TaskUnits.TaskDataModels;

namespace TaskUnits
{
    public static class TaskFacade
    {
        internal static async UniTask<ITaskData> GetTask(string token, string taskId, IProgress<float> progress = null)
        {
            var client = await ServerClientFactory.UseCredentials("cat", "cat").Create(GameConfig.ServerEnvironment);
            var task = await client.Tasks.GetTask(taskId);
            return TaskData.ConvertFrom(task);
        }
    }
}