using System;
using Cysharp.Threading.Tasks;
using Features.GameManager;
using TaskUnits.TaskDataModels;

namespace TaskUnits
{
    public static class TaskFacade
    {
        internal static async UniTask<ITaskData> GetTask(string token, string taskId, IProgress<float> progress = null)
        {
            var player = await GameConfig.GetOrCreatePlayerClient();
            var task = player.Tasks[taskId];
            var name = await task.GetName();
            var description = await task.GetDescription();

            return new TaskData
            {
                Name = name,
                Description = description
            };
        }
    }
}